using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

        private List<Player> _players = new();

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

        private Player? selectedGroupPlayer;

        public Player? SelectedGroupPlayer
        {
            get { return selectedGroupPlayer; }
            set
            {
                selectedGroupPlayer = value;
                OnPropertyChanged(nameof(SelectedGroupPlayer));
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

        public string Occupancy
        {
            get
            {
                float result;
                try
                {
                    result = (float)_players.Where(p => p.GroupNumber != null).Count() / _players.Count * 100;
                }
                catch (DivideByZeroException)
                {
                    result = 0;
                }
                return $"{result}%";
            }
        }

        public int PlayersCount => _players.Count;

        public string StatusString { get; private set; } = string.Empty;
        #endregion

        #region Command bindings
        public RelayCommand DeletePlayerFromGroupButton { get; private set; }
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
            DeletePlayerFromGroupButton = new(OnDeletePlayerFromGroupButtonClick);
            AddPlayerToGroupAButton = new(OnAddPlayerToGroupAButtonClick);
            AddPlayerToGroupBButton = new(OnAddPlayerToGroupBButtonClick);
            AddPlayerToGroupCButton = new(OnAddPlayerToGroupCButtonClick);
            AddPlayerToGroupDButton = new(OnAddPlayerToGroupDButtonClick);
            SaveButton = new(OnSaveButtonClick);
        }

        #region Command handlers
        private void OnDeletePlayerFromGroupButtonClick(object? ignorable)
        {
            Debug.WriteLine(SelectedGroupPlayer?.NickName ?? "null");
            if (selectedGroupPlayer == null)
            {
                MessageBox.Show("Выберите игрока.");
                return;
            }
            selectedGroupPlayer.GroupNumber = null;
            UpdateAllListsProperties();
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
            OnPropertyChanged(nameof(GroupASize));
            OnPropertyChanged(nameof(Occupancy));
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
            OnPropertyChanged(nameof(GroupBSize));
            OnPropertyChanged(nameof(Occupancy));
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
            OnPropertyChanged(nameof(GroupCSize));
            OnPropertyChanged(nameof(Occupancy));
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
            OnPropertyChanged(nameof(GroupDSize));
            OnPropertyChanged(nameof(Occupancy));
        }

        private async void OnSaveButtonClick(object? ignorable)
        {
            _players = await _apiInteractor.UpdatePlayersAsync(_players);
            ShowStatusString("Сохранено.");
        }
        #endregion

        private void UpdateAllListsProperties()
        {
            OnPropertyChanged(nameof(UnallocatedPlayers));
            OnPropertyChanged(nameof(GroupAPlayers));
            OnPropertyChanged(nameof(GroupBPlayers));
            OnPropertyChanged(nameof(GroupCPlayers));
            OnPropertyChanged(nameof(GroupDPlayers));
            OnPropertyChanged(nameof(GroupASize));
            OnPropertyChanged(nameof(GroupBSize));
            OnPropertyChanged(nameof(GroupCSize));
            OnPropertyChanged(nameof(GroupDSize));
            OnPropertyChanged(nameof(Occupancy));
            OnPropertyChanged(nameof(PlayersCount));
        }

        private void ShowStatusString(string status)
        {
            _ = Task.Run(async () =>
            {
                StatusString = status;
                OnPropertyChanged(nameof(StatusString));
                await Task.Delay(3000);
                StatusString = string.Empty;
                OnPropertyChanged(nameof(StatusString));
            });
        }

        public void LoadData()
        {
            _players = _apiInteractor.GetAllPlayers();
            UpdateAllListsProperties();
        }

        public async Task LoadDataAsync()
        {
            _players = await _apiInteractor.GetAllPlayersAsync();
            UpdateAllListsProperties();
        }
    }
}
