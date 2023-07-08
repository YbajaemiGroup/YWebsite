using YApi;
using YConsole.Model;
using YConsole.ViewModels;
using YConsole.ViewModels.Dialogs;
using YConsole.Views.Dialogs;

namespace YConsole;

public class Locator
{
    public YApiInteractor ApiInteractor { get; private set; }

    public MainViewModel MainViewModel { get; private set; }
    public PlayerWorkspaceViewModel PlayerWorkspaceViewModel { get; private set; }

    public Locator()
    {
        DialogService.RegisterDialog<DeleteConfirmationDialog, DeleteConfirmationViewModel>();
        ApiInteractor = new(ConfigInteractor.GetToken());
        MainViewModel = new MainViewModel();

        PlayerWorkspaceViewModel = new PlayerWorkspaceViewModel(ApiInteractor, new DialogService());
    }
}
