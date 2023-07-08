using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YApi;
using YApiModel.Models;
using YConsole.ViewModels.Dialogs;

namespace YConsole.ViewModels
{
    public class PlayerWorkspaceViewModel : ViewModelBase
    {
        private const string DEFAULT_VALUE = "??";

        #region Bindings

        public ObservableCollection<Player> Players { get; set; } = new();

        private Player? chosenPlayer;

        public Player? ChosenPlayer
        {
            get => chosenPlayer;
            set
            {
                chosenPlayer = value;
                _saved = true;
                Id = chosenPlayer?.Id?.ToString() ?? DEFAULT_VALUE;
                Nickname = chosenPlayer?.NickName ?? DEFAULT_VALUE;
                ImageName = chosenPlayer?.ImageName ?? DEFAULT_VALUE;
                Description = chosenPlayer?.Description ?? DEFAULT_VALUE;
                GroupNumber = chosenPlayer?.GroupNumber?.ToString() ?? DEFAULT_VALUE;
                Won = chosenPlayer?.Won?.ToString() ?? DEFAULT_VALUE;
                Lose = chosenPlayer?.Lose?.ToString() ?? DEFAULT_VALUE;
                Points = chosenPlayer?.Points?.ToString() ?? DEFAULT_VALUE;
            }
        }

        private string id = DEFAULT_VALUE;

        public string Id
        {
            get { return id; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Id.ToString())
                    {
                        _saved = false;
                        OnPropertyChanged(nameof(Status));
                    }
                }
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string nickname = DEFAULT_VALUE;

        public string Nickname
        {
            get { return nickname; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.NickName)
                    {
                        _saved = false;
                        OnPropertyChanged(nameof(Status));
                    }
                }
                nickname = value; 
                OnPropertyChanged(nameof(Nickname));
            }
        }

        private string imageName = DEFAULT_VALUE;

        public string ImageName
        {
            get { return imageName; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.ImageName)
                    {
                        _saved = false;
                        OnPropertyChanged(nameof(Status));
                    }
                }
                imageName = value;
                OnPropertyChanged(nameof(ImageName));
            }
        }

        private string description = DEFAULT_VALUE;

        public string Description
        {
            get { return description; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Description)
                    {
                        _saved = false;
                        OnPropertyChanged(nameof(Status));
                    }
                }
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string groupNumber = DEFAULT_VALUE;

        public string GroupNumber
        {
            get => groupNumber switch
            {
                "1" => "A",
                "2" => "B",
                "3" => "C",
                "4" => "D",
                DEFAULT_VALUE => DEFAULT_VALUE,
                _ => throw new ArgumentOutOfRangeException(nameof(groupNumber))
            };
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.GroupNumber?.ToString())
                    {
                        _saved = false;
                    }
                }
                description = value;
                OnPropertyChanged(nameof(GroupNumber));
            }
        }

        private string won = DEFAULT_VALUE;

        public string Won
        {
            get { return won; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Won?.ToString())
                    {
                        _saved = false;
                    }
                }
                won = value;
                OnPropertyChanged(nameof(Won));
            }
        }

        private string lose = DEFAULT_VALUE;

        public string Lose
        {
            get { return lose; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Lose?.ToString())
                    {
                        _saved = false;
                    }
                }
                lose = value;
                OnPropertyChanged(nameof(Lose));
            }
        }

        private string points = DEFAULT_VALUE;

        public string Points
        {
            get { return points; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Points?.ToString())
                    {
                        _saved = false;
                    }
                }
                points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        public string Status { get => _saved ? "Сохранено" : "Не сохранено"; }

        #endregion

        #region Command Bindings

        public RelayCommand SaveButton { get; private set; }
        public RelayCommand DeleteButton { get; private set; }
        public RelayCommand CreateButton { get; private set; }

        public RelayCommand ChangeImageButton { get; private set; }

        #endregion

        private readonly YApiInteractor _apiInteractor;
        private bool _saved = true;
        private readonly IDialogService _dialogService;

        public PlayerWorkspaceViewModel(YApiInteractor apiInteractor, IDialogService dialogService)
        {
            this._apiInteractor = apiInteractor;
            _ = Task.Run(async () => Players = new(await apiInteractor.GetAllPlayersAsync()));
            _dialogService = dialogService;
            SaveButton = new(OnSaveButtonClick);
            DeleteButton = new(OnDeleteButtonClick);
            CreateButton = new(OnCreateButtonClick);
            ChangeImageButton = new(OnChangeImageButtonClick);
        }

        #region Command handlers

        private async void OnSaveButtonClick(object? ignorable)
        {
            if (_saved)
                return;
            Players = new(await _apiInteractor.UpdatePlayersAsync(Players.ToList()));
            _saved = true;
        }

        private async void OnDeleteButtonClick(object? ignorable)
        {
            bool delete = false;
            _dialogService.ShowDialog<DeleteConfirmationViewModel>(result => delete = result);
            if (!delete)
            {
                return;
            }
            else
            {
                if (ChosenPlayer?.Id == null)
                {
                    MessageBox.Show("Выберите игрока");
                    return;
                }
                await _apiInteractor.DeletePlayer(ChosenPlayer.Id.Value);
                ChosenPlayer = null;
            }
            throw new NotImplementedException();
        }

        private void OnCreateButtonClick(object? ignorable)
        {
            throw new NotImplementedException();

        }

        private void OnChangeImageButtonClick(object? ignorable)
        {
            throw new NotImplementedException();

        }

        #endregion

    }
}
