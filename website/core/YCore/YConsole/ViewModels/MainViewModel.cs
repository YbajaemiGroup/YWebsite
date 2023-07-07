﻿namespace YConsole.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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

        public MainViewModel()
        {
            OpenPlayersWorkspaceCommand = new(OnPlayersWorkspaceCommandClick);
        }

        #region Command bindings

        public RelayCommand OpenPlayersWorkspaceCommand { get; private set; }

        #endregion

        #region Command handlers

        private void OnPlayersWorkspaceCommandClick(object? ignorable)
        {
            Workspace = new PlayerWorkspaceViewModel();
        }

        #endregion
    }
}
