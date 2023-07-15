using Microsoft.Extensions.DependencyInjection;
using YApi;
using YConsole.ViewModels;
using YConsole.ViewModels.Dialogs;

namespace YConsole;

public class Locator
{
    public IApiInteractor? ApiInteractor { get; private set; } = App._Host.Services.GetRequiredService<IApiInteractor>();

    public MainViewModel? MainViewModel { get; private set; } = App._Host.Services.GetRequiredService<MainViewModel>();
    public PlayerWorkspaceViewModel? PlayerWorkspaceViewModel { get; private set; } = App._Host.Services.GetRequiredService<PlayerWorkspaceViewModel>();
    public ImageDialogViewModel? ImageDialogViewModel { get; private set; } = App._Host.Services.GetRequiredService<ImageDialogViewModel>();
    public TokenCreateViewModel? TokenCreateViewModel { get; private set; } = App._Host.Services.GetRequiredService<TokenCreateViewModel>();
    public TokenDeleteViewModel? TokenDeleteViewModel { get; private set; } = App._Host.Services.GetRequiredService<TokenDeleteViewModel>();
}
