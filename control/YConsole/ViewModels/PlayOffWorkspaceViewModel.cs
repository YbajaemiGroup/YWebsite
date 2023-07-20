using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using YApi;
using YApiModel.Models;
using YConsole.Model;
using YConsole.Views.Utils;

namespace YConsole.ViewModels
{
    public class PlayOffWorkspaceViewModel : ViewModelBase, IDataLoadable
    {
        public PlayerCommand PlayerCommand { get; set; }

        private List<Player> _players = new();
        private Bracket bracket = null!;
        private ObservableCollection<Player> players = new();

        public ObservableCollection<Player> Players
        {
            get => players;
            set
            {
                players = value;
                OnPropertyChanged(nameof(Players));
            }
        }

        public RelayCommand SaveButton { get; private set; }

        private readonly IApiInteractor _apiInteractor;

        public PlayOffWorkspaceViewModel(IApiInteractor apiInteractor)
        {
            _apiInteractor = apiInteractor;
            SaveButton = new(OnSaveButtonClick);
            PlayerCommand = new(OnPlayerChanged);
        }

        private void OnPlayerChanged(int playerDescriptor, int roundDescriptor, bool isUpper, object value)
        {
            if (value is Player player && player?.Id != null)
            {
                bracket.SetPlayer(playerDescriptor, roundDescriptor, isUpper, player.Id.Value);
            }
        }

        private void OnSaveButtonClick(object? ignorable)
        {
            bracket.SetWinners();
            _ = Task.Run(async () =>
            {
                await _apiInteractor.PostBracketAsync(bracket.Rounds);
                MessageBox.Show("Дааные отправлены.");
            });
        }

        public void LoadData()
        {
            _players = _apiInteractor.GetAllPlayers();
            players = new(_players);
            OnPropertyChanged(nameof(Players));
            var rounds = _apiInteractor.GetBracketAsync().Result;
            bracket = new(rounds, _players);
            bracket.ForEach(PlayerCommand.UpdateData);
        }

        public async Task LoadDataAsync()
        {
            _players = await _apiInteractor.GetAllPlayersAsync();
            players = new(_players);
            OnPropertyChanged(nameof(Players));
            var rounds = await _apiInteractor.GetBracketAsync();
            bracket = new(rounds, _players);
            bracket.ForEach(PlayerCommand.UpdateData);
        }
    }
}
