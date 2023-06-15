using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YCore
{
    delegate void OnRequestReceivedHandler(HttpListenerContext context);

    internal class Core : IDisposable
    {
        public HttpListener HttpListener { get; private set; }

        public event OnRequestReceivedHandler? RequestReceived;

        private readonly CancellationTokenSource cancellationTokenSource;

        public Core(string ipAddress, int port)
        {
            HttpListener = new();
            HttpListener.Prefixes.Add($"http://{ipAddress}:{port}/api/");
            cancellationTokenSource = new();
        }

        public void Start()
        {
            Logger.Log(LogSeverity.Info, nameof(Core), "Starting listener");
            try
            {
                HttpListener.Start();
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Error, nameof(Core), "Can not start tcp listener.", e);
                throw;
            }
            Logger.Log(LogSeverity.Info, nameof(Core), "Listener started");
            _ = Task.Run(StartMainLoop, cancellationTokenSource.Token);
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            Logger.Log(LogSeverity.Info, nameof(Core), "Stopping main loop");
            HttpListener?.Stop();
            Logger.Log(LogSeverity.Info, nameof(Core), "Listener stopped");
        }

        public void Dispose()
        {
            Stop();
            (HttpListener as IDisposable).Dispose();
        }

        private void StartMainLoop()
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                var context = HttpListener.GetContext();
                Logger.Log(LogSeverity.Debug, nameof(Core), "Context\n" + context.ToString());
                RequestReceived?.Invoke(context);
            }
        }
    }
}
