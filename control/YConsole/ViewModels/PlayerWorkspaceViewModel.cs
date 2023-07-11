﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            get => ChosenPlayer?.Id ?? 0;
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
            get => ChosenPlayer?.NickName ?? "??";
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
            get => ChosenPlayer?.ImageName ?? "??";
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
            get => ChosenPlayer?.Description ?? "??";
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
            get => ChosenPlayer?.GroupNumber ?? 0;
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
            get => ChosenPlayer?.Won ?? 0;
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
            get => ChosenPlayer?.Lose ?? 0;
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
            get => ChosenPlayer?.Points ?? 0;
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

        public RelayCommand IncreaseWinsButton { get; private set; }
        public RelayCommand DecreaseWinsButton { get; private set; }
        public RelayCommand IncreaseLosesButton { get; private set; }
        public RelayCommand DecreaseLosesButton { get; private set; }
        public RelayCommand IncreasePointsButton { get; private set; }
        public RelayCommand DecreasePointsButton { get; private set; }

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
            IncreaseWinsButton = new(OnIncreaseWinsButtonClick);
            DecreaseWinsButton = new(OnDecreaseWinsButtonClick);
            IncreaseLosesButton = new(OnIncreaseLosesButtonClick);
            DecreaseLosesButton = new(OnDecreaseLosesButtonClick);
            IncreasePointsButton = new(OnIncreasePointsButtonClick);
            DecreasePointsButton = new(OnDecreasePointsButtonClick);
        }

        #region Command handlers

        private async void OnSaveButtonClick(object? ignorable)
        {
            if (_saved)
                return;
            try
            {
                Players = new(await _apiInteractor.UpdatePlayersAsync(Players.ToList()));
                _saved = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
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
            var newPlayer = new Player("Новый игрок", "Описание");
            Players.Add(newPlayer);
            ChosenPlayer = newPlayer;
            _saved = false;
        }

        private void OnChangeImageButtonClick(object? ignorable)
        {
            throw new NotImplementedException();
        }

        private void OnIncreaseWinsButtonClick(object? ignorable)
        {
            Won++;
            Points++;
        }

        private void OnDecreaseWinsButtonClick(object? ignorable)
        {
            if (Won <= 0)
                return;
            Won--;
            Points--;
        }

        private void OnIncreaseLosesButtonClick(object? ignorable)
        {
            Lose++;
            Points++;
        }

        private void OnDecreaseLosesButtonClick(object? ignorable)
        {
            if (Lose <= 0)
                return;
            Lose--;
            Points--;
        }

        private void OnIncreasePointsButtonClick(object? ignorable)
        {
            Points++;
        }

        private void OnDecreasePointsButtonClick(object? ignorable)
        {
            if (Points <= 0)
                return;
            Points--;
        }

        #endregion

    }
}
