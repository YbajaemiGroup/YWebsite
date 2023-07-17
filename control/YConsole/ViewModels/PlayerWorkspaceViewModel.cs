using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YApi;
using YApiModel.Models;
using YConsole.Utillities;
using YConsole.ViewModels.Dialogs;

namespace YConsole.ViewModels
{
    public class PlayerWorkspaceViewModel : ViewModelBase, IDataLoadable
    {
        #region Bindings

        private ObservableCollection<Player> players = new();

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged(nameof(Players));
            }
        }

        private Player? chosenPlayer;

        public Player? ChosenPlayer
        {
            get => chosenPlayer;
            set
            {
                chosenPlayer = value;
                Id = chosenPlayer?.Id;
                Nickname = chosenPlayer?.NickName;
                ImageName = chosenPlayer?.ImageName;
                Description = chosenPlayer?.Description;
                GroupNumber = chosenPlayer?.GroupNumber;
                Won = chosenPlayer?.Won;
                Lose = chosenPlayer?.Lose;
                Points = chosenPlayer?.Points;
                OnPropertyChanged(nameof(ChosenPlayer));
            }
        }

        public int? Id
        {
            get => ChosenPlayer?.Id;
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
                    return;
                }
                ChosenPlayer.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string? Nickname
        {
            get => ChosenPlayer?.NickName;
            set
            {
                if (value == null)
                {
                    MessageBox.Show("Имя игрока не может быть пустым");
                    return;
                }
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
                    return;
                }
                ChosenPlayer.NickName = value;
                OnPropertyChanged(nameof(Nickname));
            }
        }

        public string? SelectedImagePath
        {
            get
            {
                string fullName = $"{_imagesPath}\\{ImageName}";
                if (string.IsNullOrEmpty(ImageName) || !File.Exists(fullName))
                {
                    return null;
                }
                return fullName;
            }
        }

        public string? ImageName
        {
            get => ChosenPlayer?.ImageName;
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
                    return;
                }
                ChosenPlayer.ImageName = value;
                OnPropertyChanged(nameof(ImageName));
                OnPropertyChanged(nameof(SelectedImagePath));
            }
        }

        public string? Description
        {
            get => ChosenPlayer?.Description;
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
                    return;
                }
                ChosenPlayer.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public int? GroupNumber
        {
            get => ChosenPlayer?.GroupNumber;
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
                    return;
                }
                ChosenPlayer.GroupNumber = value;
                OnPropertyChanged(nameof(GroupNumber));
            }
        }

        public int? Won
        {
            get => ChosenPlayer?.Won;
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
                    return;
                }
                ChosenPlayer.Won = value;
                OnPropertyChanged(nameof(Won));
            }
        }

        public int? Lose
        {
            get => ChosenPlayer?.Lose;
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
                    return;
                }
                ChosenPlayer.Lose = value;
                OnPropertyChanged(nameof(Lose));
            }
        }

        public int? Points
        {
            get => ChosenPlayer?.Points;
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

        private readonly IApiInteractor _apiInteractor;
        private bool _saved = true;
        private readonly IWindowService _windowService;
        private readonly IDialogService _dialogService;
        private readonly IConfigInteractor _configInteractor;
        private readonly string _imagesPath;

        public PlayerWorkspaceViewModel(IApiInteractor apiInteractor,
                                        IWindowService windowService,
                                        IDialogService dialogService,
                                        IConfigInteractor configInteractor)
        {
            _apiInteractor = apiInteractor;
            _windowService = windowService;
            _dialogService = dialogService;
            _configInteractor = configInteractor;
            _imagesPath = configInteractor.GetImagesLocation();
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
                OnPropertyChanged(nameof(Status));
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
            _dialogService.ShowDialog<DeleteConfirmationDialogViewModel>(result => delete = result, $"Удалить игрока {ChosenPlayer?.NickName}?");
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
            OnPropertyChanged(nameof(Status));
        }

        private void OnChangeImageButtonClick(object? ignorable)
        {
            var imageDialogViewModel = _windowService.Show<ImageDialogViewModel>();
            imageDialogViewModel.OnImageUpdated += imageName => ImageName = imageName;
            _ = imageDialogViewModel.LoadDataAsync();
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

        public void LoadData()
        {
            try
            {
                Players = new(_apiInteractor.GetAllPlayersAsync().Result);
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка при загрузке игроков.");
                Players = new();
            }
        }

        public async Task LoadDataAsync()
        {
            try
            {
                Players = new(await _apiInteractor.GetAllPlayersAsync());
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка при загрузке игроков.");
                Players = new();
            }
        }

        #endregion

    }
}
