using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using YApi;
using YConsole.Model;
using YConsole.Utillities;
using YConsole.ViewModels;
using YConsole.ViewModels.Dialogs;
using YConsole.Views;
using YConsole.Views.Dialogs;

namespace YConsole
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly IHost _Host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(new YApiInteractor(ConfigInteractor.GetToken()));

                    services.AddSingleton<IDialogService, DialogService>();
                    services.AddSingleton<IWindowService, WindowService>(serviceProvider => new WindowService(serviceProvider));

                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<PlayerWorkspace>();
                    services.AddTransient<ImagesDialogWindow>();
                    services.AddTransient<CreateTokenWindow>();
                    services.AddTransient<DeleteTokenWindow>();

                    services.AddSingleton<Locator>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<PlayerWorkspaceViewModel>();
                    services.AddSingleton<ImageDialogViewModel>();
                    services.AddSingleton<TokenCreateViewModel>();
                    services.AddSingleton<TokenDeleteViewModel>();
                }).Build();

        protected override void OnStartup(StartupEventArgs e)
        {
            _Host.Start();
            RegisterWindowServices();
            RegisterDialogServices();
            MainWindow = _Host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _Host.StopAsync();
            _Host.Dispose();
            base.OnExit(e);
        }

        private static void RegisterDialogServices()
        {
            DialogService.RegisterDialog<DeleteConfirmationDialog, DeleteConfirmationDialogViewModel>();
            DialogService.RegisterDialog<ReplaceImageDialog, ReplaceImageDialogViewModel>();
        }

        private static void RegisterWindowServices()
        {
            WindowService.RegisterView<ImageDialogViewModel, ImagesDialogWindow>();
            WindowService.RegisterView<TokenCreateViewModel, CreateTokenWindow>();
            WindowService.RegisterView<TokenDeleteViewModel, DeleteTokenWindow>();
        }
    }
}
