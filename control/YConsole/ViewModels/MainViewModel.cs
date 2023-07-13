using YConsole.ViewModels.Dialogs;
using YConsole.Views;

namespace YConsole.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;
        private readonly PlayerWorkspaceViewModel _playerWorkspaceViewModel;

        private ViewModelBase? workspace;
        public ViewModelBase? Workspace
        {
            get => workspace;
            set
            {
                workspace = value;
                OnPropertyChanged(nameof(Workspace));
            }
        }

        public MainViewModel(IWindowService windowService, PlayerWorkspaceViewModel playerWorkspaceViewModel)
        {
            _windowService = windowService;
            OpenPlayersWorkspaceCommand = new(OnPlayersWorkspaceCommandClick);
            _playerWorkspaceViewModel = playerWorkspaceViewModel;
            OpenTokenCreateCommand = new(OnTokenCreateWorkspaceClick);
            OpenTokenDeleteCommand = new(OnTokenDeleteWorkspaceClick);
        }

        #region Command bindings

        public RelayCommand OpenPlayersWorkspaceCommand { get; private set; }
        public RelayCommand OpenTokenCreateCommand { get; private set; }
        public RelayCommand OpenTokenDeleteCommand { get; private set; }

        #endregion

        #region Command handlers

        private void OnPlayersWorkspaceCommandClick(object? ignorable)
        {
            Workspace = _playerWorkspaceViewModel;
            _ = _playerWorkspaceViewModel.LoadDataAsync();
        }

        private void OnTokenCreateWorkspaceClick (object? ignorable)
        {
            _windowService.Show<TokenCreateViewModel>();
        }
        private void OnTokenDeleteWorkspaceClick (object? ignorable)
        {
            _windowService.Show<TokenDeleteViewModel>();
        }

        #endregion
    }
}
