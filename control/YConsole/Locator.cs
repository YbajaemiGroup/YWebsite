using Microsoft.Extensions.DependencyInjection;
using YApi;
using YConsole.ViewModels;

namespace YConsole;

public class Locator
{
    public YApiInteractor? ApiInteractor { get; private set; } = App._Host.Services.GetService<YApiInteractor>();

    public MainViewModel? MainViewModel { get; private set; } = App._Host.Services.GetService<MainViewModel>();
    public PlayerWorkspaceViewModel? PlayerWorkspaceViewModel { get; private set; } = App._Host.Services.GetService<PlayerWorkspaceViewModel>();
}
