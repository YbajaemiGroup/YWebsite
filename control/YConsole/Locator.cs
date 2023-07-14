using Microsoft.Extensions.DependencyInjection;
using YApi;
using YConsole.ViewModels;
using YConsole.ViewModels.Dialogs;

namespace YConsole;

public class Locator
{
    public YApiInteractor? ApiInteractor { get; private set; } = App._Host.Services.GetService<YApiInteractor>();

    public MainViewModel? MainViewModel { get; private set; } = App._Host.Services.GetService<MainViewModel>();
    public PlayerWorkspaceViewModel? PlayerWorkspaceViewModel { get; private set; } = App._Host.Services.GetService<PlayerWorkspaceViewModel>();
    public LinkWorkspaceViewModel? LinkWorkspaceViewModel { get; private set; } = App._Host.Services.GetService<LinkWorkspaceViewModel>();
    public ImageDialogViewModel? ImageDialogViewModel { get; private set; } = App._Host.Services.GetService<ImageDialogViewModel>();
    public TokenCreateViewModel? TokenCreateViewModel { get; private set; } = App._Host.Services.GetService<TokenCreateViewModel>();
    public TokenDeleteViewModel? TokenDeleteViewModel { get; private set; } = App._Host.Services.GetService<TokenDeleteViewModel>();
}
