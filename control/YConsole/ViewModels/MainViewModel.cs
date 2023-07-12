namespace YConsole.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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

        public MainViewModel(PlayerWorkspaceViewModel playerWorkspaceViewModel)
        {
            OpenPlayersWorkspaceCommand = new(OnPlayersWorkspaceCommandClick);
            _playerWorkspaceViewModel = playerWorkspaceViewModel;
        }

        #region Command bindings

        public RelayCommand OpenPlayersWorkspaceCommand { get; private set; }

        #endregion

        #region Command handlers

        private void OnPlayersWorkspaceCommandClick(object? ignorable)
        {
            Workspace = _playerWorkspaceViewModel;
            _ = _playerWorkspaceViewModel.LoadDataAsync();
        }

        #endregion
    }
}
