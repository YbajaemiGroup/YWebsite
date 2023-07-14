using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using YApi;
using YConsole.Model;
using YConsole.Utillities;

namespace YConsole.ViewModels.Dialogs
{
    public class ImageDialogViewModel : ViewModelBase, IDataLoadable
    {
        public class ImageListObject
        {
            public string? ImageName { get; set; }
            public string? ImagePath { get; set; }
        }

        #region Bindings

        private ObservableCollection<ImageListObject> databaseImagesPaths = new();

        public ObservableCollection<ImageListObject> DatabaseImagesPaths
        {
            get { return databaseImagesPaths; }
            set
            {
                databaseImagesPaths = value;
                OnPropertyChanged(nameof(DatabaseImagesPaths));
            }
        }

        private ImageListObject? selectedImage;

        public ImageListObject? SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        #endregion

        #region Command bindings

        public RelayCommand AddButton { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        #endregion

        public readonly string _imageSource;
        private readonly YApiInteractor _apiInteractor;
        private readonly IDialogService _dialogService;
        public event Action<string>? OnImageUpdated;

        public ImageDialogViewModel(YApiInteractor apiInteractor, IDialogService dialogService, IConfigInteractor configInteractor)
        {
            _apiInteractor = apiInteractor;
            _dialogService = dialogService;
            _imageSource = configInteractor.GetImagesLocation();
            AddButton = new(OnAddButtonClick);
            SaveCommand = new(OnSaveButtonClick);
        }

        public void LoadData()
        {
            var images = _apiInteractor.DownloadImages(_imageSource);
            foreach (var image in images)
            {
                DatabaseImagesPaths.Add(new()
                {
                    ImageName = image.Split('\\')[^1],
                    ImagePath = $"{_imageSource}\\{image}"
                });
                OnPropertyChanged(nameof(DatabaseImagesPaths));
            }
        }

        public async Task LoadDataAsync()
        {
            await foreach (var image in _apiInteractor.DownloadAllImagesAsync(_imageSource))
            {
                DatabaseImagesPaths.Add(new()
                {
                    ImageName = image.Split('\\')[^1],
                    ImagePath = $"{_imageSource}\\{image}"
                });
                OnPropertyChanged(nameof(DatabaseImagesPaths));
            }
        }

        #region Command handlers

        private async void OnAddButtonClick(object? ignorable)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }
            var fileName = openFileDialog.FileName.Split('\\')[^1];
            if (File.Exists($"{_imageSource}\\{fileName}"))
            {
                MessageBox.Show($"Файл {fileName} уже существует в папке {_imageSource}.");
                var dvm = new ReplaceImageDialogViewModel($"Заменить файл {fileName} в папке назначения?");
                bool answer = false;
                _dialogService.ShowDialog(dvm, res => answer = res);
                if (!answer)
                {
                    return;
                }
                File.Delete($"{_imageSource}\\{fileName}");
            }
            var image = File.OpenRead(openFileDialog.FileName);
            if (image == null)
            {
                MessageBox.Show($"Невозможно прочитать файл {openFileDialog.FileName}.");
                return;
            }
            var loadingImageToServer = _apiInteractor.LoadImageToServerAsync(fileName, image);
            try
            {
                File.Copy(openFileDialog.FileName, $"{_imageSource}\\{fileName}");
            }
            catch (Exception)
            {
                MessageBox.Show($"Произошла ошибка при перемещении файла {fileName} в локальную папку с изображениями.");
                return;
            }
            await loadingImageToServer;
            if (!loadingImageToServer.IsCompletedSuccessfully)
            {
                MessageBox.Show(loadingImageToServer.Exception?.Message);
            }
        }

        private void OnSaveButtonClick(object? ignorable)
        {
            
        }

        #endregion
    }
}
