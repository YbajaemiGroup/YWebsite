using YApi;
using YConsole.Model;
using YConsole.ViewModels;

namespace YConsole;

public class Locator
{
    public YApiInteractor ApiInteractor { get; private set; }

    public MainViewModel MainViewModel { get; private set; }
    public PlayerWorkspaceViewModel PlayerWorkspaceViewModel { get; private set; }

    public Locator()
    {
        ApiInteractor = new(ConfigInteractor.GetToken());
        MainViewModel = new MainViewModel();
        PlayerWorkspaceViewModel = new PlayerWorkspaceViewModel(ApiInteractor);
    }
}
