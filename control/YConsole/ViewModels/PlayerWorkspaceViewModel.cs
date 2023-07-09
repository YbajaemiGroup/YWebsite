using Accessibility;
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
                Id = chosenPlayer?.Id ?? 0;
                Nickname = chosenPlayer?.NickName ?? DEFAULT_VALUE;
                ImageName = chosenPlayer?.ImageName ?? DEFAULT_VALUE;
                Description = chosenPlayer?.Description ?? DEFAULT_VALUE;
                GroupNumber = chosenPlayer?.GroupNumber ?? 0;
                Won = chosenPlayer?.Won ?? 0;
                Lose = chosenPlayer?.Lose ?? 0;
                Points = chosenPlayer?.Points ?? 0;
            }
        }

        public int Id
        {
            get { return ChosenPlayer?.Id ?? 0; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Id)
                    {
                        _saved = false;
                        OnPropertyChanged(nameof(Status));
                    }
                }
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Nickname
        {
            get { return ChosenPlayer?.NickName ?? "??"; }
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
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.NickName = value; 
                OnPropertyChanged(nameof(Nickname));
            }
        }

        public string ImageName
        {
            get { return ChosenPlayer?.ImageName ?? "??"; }
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
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.ImageName = value;
                OnPropertyChanged(nameof(ImageName));
            }
        }

        public string Description
        {
            get { return ChosenPlayer?.Description ?? "??"; }
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
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public int GroupNumber
        {
            get { return ChosenPlayer?.GroupNumber ?? 0; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.GroupNumber)
                    {
                        _saved = false;
                    }
                }
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.GroupNumber = value;
                OnPropertyChanged(nameof(GroupNumber));
            }
        }

        public int Won
        {
            get { return ChosenPlayer?.Won ?? 0; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Won)
                    {
                        _saved = false;
                    }
                }
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.Won = value;
                OnPropertyChanged(nameof(Won));
            }
        }

        public int Lose
        {
            get { return ChosenPlayer?.Lose ?? 0; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Lose)
                    {
                        _saved = false;
                    }
                }
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.Lose = value;
                OnPropertyChanged(nameof(Lose));
            }
        }

        public int Points
        {
            get { return ChosenPlayer?.Points ?? 0; }
            set
            {
                if (_saved)
                {
                    if (value != ChosenPlayer?.Points)
                    {
                        _saved = false;
                    }
                }
                if (ChosenPlayer == null)
                {
                    MessageBox.Show("Выберите игрока.");
                    return;
                }
                ChosenPlayer.Points = value;
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
            _apiInteractor = apiInteractor;
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
            if (ChosenPlayer?.Id == null)
            {
                MessageBox.Show("Выберите игрока");
                return;
            }
            await _apiInteractor.DeletePlayer(ChosenPlayer.Id.Value);
            Players.Remove(ChosenPlayer);
            ChosenPlayer = null;
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
