using YConsole.Utillities;
using YConsole.ViewModels.Dialogs;

namespace YConsole.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;
        private readonly PlayerWorkspaceViewModel _playerWorkspaceViewModel;
        private readonly LinkWorkspaceViewModel _linkWorkspaceViewModel;
        private readonly GroupsWorkspaceViewModel _groupsWorkspaceViewModel;

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

        public MainViewModel(IWindowService windowService,
                             PlayerWorkspaceViewModel playerWorkspaceViewModel,
                             LinkWorkspaceViewModel linkWorkspaceViewModel,
                             GroupsWorkspaceViewModel groupsWorkspaceViewModel)
        {
            _windowService = windowService;
            _playerWorkspaceViewModel = playerWorkspaceViewModel;
            _linkWorkspaceViewModel = linkWorkspaceViewModel;
            _groupsWorkspaceViewModel = groupsWorkspaceViewModel;
            OpenPlayersWorkspaceCommand = new(OnPlayersWorkspaceCommandClick);
            OpenLinksWorkspaceCommand = new(OnLinksWorkspaceCommandClick);
            OpenGroupsWorkspaceCommand = new(OnGroupsWorkspaceCommandClick);
            OpenTokenCreateCommand = new(OnTokenCreateWorkspaceClick);
            OpenTokenDeleteCommand = new(OnTokenDeleteWorkspaceClick);
        }

        #region Command bindings

        public RelayCommand OpenPlayersWorkspaceCommand { get; private set; }
        public RelayCommand OpenLinksWorkspaceCommand { get; private set; }
        public RelayCommand OpenGroupsWorkspaceCommand { get; private set; }
        public RelayCommand OpenTokenCreateCommand { get; private set; }
        public RelayCommand OpenTokenDeleteCommand { get; private set; }

        #endregion

        #region Command handlers

        private void OnPlayersWorkspaceCommandClick(object? ignorable)
        {
            Workspace = _playerWorkspaceViewModel;
            _ = _playerWorkspaceViewModel.LoadDataAsync();
        }

        private void OnLinksWorkspaceCommandClick(object? ignorable)
        {
            Workspace = _linkWorkspaceViewModel;
            _ = _linkWorkspaceViewModel.LoadDataAsync();
        }

        private void OnGroupsWorkspaceCommandClick(object? ignorable)
        {
            Workspace = _groupsWorkspaceViewModel;
            _ = _groupsWorkspaceViewModel.LoadDataAsync();
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
