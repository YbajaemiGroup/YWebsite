using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using YApi;
using YApiModel.Models;

namespace YConsole.ViewModels
{
    public class GroupsWorkspaceViewModel : ViewModelBase, IDataLoadable
    {
        private const int GROUP_A_NUMBER = 1;
        private const int GROUP_B_NUMBER = 2;
        private const int GROUP_C_NUMBER = 3;
        private const int GROUP_D_NUMBER = 4;

        private List<Player> _players = null!;

        #region Bindings
        private Player? selectedUnallocatedPlayer;

        public Player? SelectedUnallocatedPlayer
        {
            get { return selectedUnallocatedPlayer; }
            set
            {
                selectedUnallocatedPlayer = value;
                OnPropertyChanged(nameof(SelectedUnallocatedPlayer));
            }
        }

        public ObservableCollection<Player> UnallocatedPlayers => new(_players.Where(p => p.GroupNumber == null));

        private Player? selectedGroupAPlayer;

        public Player? SelectedGroupAPlayer
        {
            get { return selectedGroupAPlayer; }
            set
            {
                selectedGroupAPlayer = value;
                OnPropertyChanged(nameof(SelectedGroupAPlayer));
            }
        }

        private Player? selectedGroupBPlayer;

        public Player? SelectedGroupBPlayer
        {
            get { return selectedGroupBPlayer; }
            set
            {
                selectedGroupBPlayer = value;
                OnPropertyChanged(nameof(SelectedGroupBPlayer));
            }
        }

        private Player? selectedGroupCPlayer;

        public Player? SelectedGroupCPlayer
        {
            get { return selectedGroupCPlayer; }
            set
            {
                selectedGroupCPlayer = value;
                OnPropertyChanged(nameof(SelectedGroupCPlayer));
            }
        }

        private Player? selectedGroupDPlayer;

        public Player? SelectedGroupDPlayer
        {
            get { return selectedGroupDPlayer; }
            set
            {
                selectedGroupDPlayer = value;
                OnPropertyChanged(nameof(SelectedGroupDPlayer));
            }
        }

        public ObservableCollection<Player> GroupAPlayers => new(_players.Where(p => p.GroupNumber == 1));
        public ObservableCollection<Player> GroupBPlayers => new(_players.Where(p => p.GroupNumber == 2));
        public ObservableCollection<Player> GroupCPlayers => new(_players.Where(p => p.GroupNumber == 3));
        public ObservableCollection<Player> GroupDPlayers => new(_players.Where(p => p.GroupNumber == 4));

        public int GroupASize => GroupAPlayers.Count;
        public int GroupBSize => GroupBPlayers.Count;
        public int GroupCSize => GroupCPlayers.Count;
        public int GroupDSize => GroupDPlayers.Count;

        public int Occupancy => throw new NotImplementedException();
        #endregion

        #region Command bindings
        public RelayCommand DeletePlayerFromGroupAButton { get; private set; }
        public RelayCommand DeletePlayerFromGroupBButton { get; private set; }
        public RelayCommand DeletePlayerFromGroupCButton { get; private set; }
        public RelayCommand DeletePlayerFromGroupDButton { get; private set; }
        public RelayCommand AddPlayerToGroupAButton { get; private set; }
        public RelayCommand AddPlayerToGroupBButton { get; private set; }
        public RelayCommand AddPlayerToGroupCButton { get; private set; }
        public RelayCommand AddPlayerToGroupDButton { get; private set; }
        public RelayCommand SaveButton { get; private set; }
        #endregion

        private readonly IApiInteractor _apiInteractor;

        public GroupsWorkspaceViewModel(IApiInteractor apiInteractor)
        {
            _apiInteractor = apiInteractor;
            DeletePlayerFromGroupAButton = new(OnDeletePlayerFromGroupAButtonClick);
            DeletePlayerFromGroupBButton = new(OnDeletePlayerFromGroupBButtonClick);
            DeletePlayerFromGroupCButton = new(OnDeletePlayerFromGroupCButtonClick);
            DeletePlayerFromGroupDButton = new(OnDeletePlayerFromGroupDButtonClick);
            AddPlayerToGroupAButton = new(OnAddPlayerToGroupAButtonClick);
            AddPlayerToGroupBButton = new(OnAddPlayerToGroupBButtonClick);
            AddPlayerToGroupCButton = new(OnAddPlayerToGroupCButtonClick);
            AddPlayerToGroupDButton = new(OnAddPlayerToGroupDButtonClick);
            SaveButton = new(OnSaveButtonClick);
        }

        #region Command handlers
        private void OnDeletePlayerFromGroupAButtonClick(object? ignorable)
        {
            throw new NotImplementedException();
        }

        private void OnDeletePlayerFromGroupBButtonClick(object? ignorable)
        {
            throw new NotImplementedException();
        }

        private void OnDeletePlayerFromGroupCButtonClick(object? ignorable)
        {
            throw new NotImplementedException();
        }

        private void OnDeletePlayerFromGroupDButtonClick(object? ignorable)
        {
            throw new NotImplementedException();
        }

        private void OnAddPlayerToGroupAButtonClick(object? ignorable)
        {
            if (SelectedUnallocatedPlayer == null)
            {
                MessageBox.Show("Выберите игрока.");
                return;
            }
            SelectedUnallocatedPlayer.GroupNumber = GROUP_A_NUMBER;
            OnPropertyChanged(nameof(UnallocatedPlayers));
            OnPropertyChanged(nameof(GroupAPlayers));
        }

        private void OnAddPlayerToGroupBButtonClick(object? ignorable)
        {
            if (SelectedUnallocatedPlayer == null)
            {
                MessageBox.Show("Выберите игрока.");
                return;
            }
            SelectedUnallocatedPlayer.GroupNumber = GROUP_B_NUMBER;
            OnPropertyChanged(nameof(UnallocatedPlayers));
            OnPropertyChanged(nameof(GroupBPlayers));
        }

        private void OnAddPlayerToGroupCButtonClick(object? ignorable)
        {
            if (SelectedUnallocatedPlayer == null)
            {
                MessageBox.Show("Выберите игрока.");
                return;
            }
            SelectedUnallocatedPlayer.GroupNumber = GROUP_C_NUMBER;
            OnPropertyChanged(nameof(UnallocatedPlayers));
            OnPropertyChanged(nameof(GroupCPlayers));
        }

        private void OnAddPlayerToGroupDButtonClick(object? ignorable)
        {
            if (SelectedUnallocatedPlayer == null)
            {
                MessageBox.Show("Выберите игрока.");
                return;
            }
            SelectedUnallocatedPlayer.GroupNumber = GROUP_D_NUMBER;
            OnPropertyChanged(nameof(UnallocatedPlayers));
            OnPropertyChanged(nameof(GroupDPlayers));
        }

        private void OnSaveButtonClick(object? ignorable)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void LoadData()
        {
            _players = _apiInteractor.GetAllPlayers();
        }

        public async Task LoadDataAsync()
        {
            _players = await _apiInteractor.GetAllPlayersAsync();
        }
    }
}
