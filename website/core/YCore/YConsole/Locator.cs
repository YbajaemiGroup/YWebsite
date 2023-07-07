using YConsole.ViewModels;

namespace YConsole;

public class Locator
{
    public MainViewModel MainViewModel { get; private set; }
    public PlayerWorkspaceViewModel PlayerWorkspaceViewModel { get; private set; }

    public Locator()
    {
        MainViewModel = new MainViewModel();
        PlayerWorkspaceViewModel = new PlayerWorkspaceViewModel();
    }
}
