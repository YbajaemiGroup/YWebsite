﻿using System.Net;
using YCore.Data;

namespace YCore
{
    public delegate void OnRequestReceivedHandler(HttpListenerContext context);

    public class Core : IDisposable
    {
        public HttpListener HttpListener { get; private set; }
        private readonly Configuration configuration;

        public event OnRequestReceivedHandler? RequestReceived;

        private readonly CancellationTokenSource cancellationTokenSource;

        public Core(Configuration configuration)
        {
            HttpListener = new();
            this.configuration = configuration;
            configuration.ApiListenAddresses.ForEach(HttpListener.Prefixes.Add);
            configuration.HttpListenAddresses.ForEach(HttpListener.Prefixes.Add);
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
            try
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    RequestReceived?.Invoke(HttpListener.GetContext());
                }
            }
            catch (HttpListenerException e)
            {
                Logger.Log(LogSeverity.Warning, nameof(Core), "Expected exception on closing sockets.", e);
                HttpListener.Stop();
                HttpListener = new HttpListener();
                configuration.ApiListenAddresses.ForEach(HttpListener.Prefixes.Add);
                configuration.HttpListenAddresses.ForEach(HttpListener.Prefixes.Add);
                HttpListener.Start();
                Logger.Log(LogSeverity.Info, nameof(Core), "Listener recreated.");
            }
        }
    }
}
