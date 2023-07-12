namespace YConsole.Views;

public interface IWindowService
{
    public void Show<IViewModel>() where IViewModel : ViewModelBase;
    public void Show(ViewModelBase viewModel);
}
