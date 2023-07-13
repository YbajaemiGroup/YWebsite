using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Headers;
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

        public string ImageSource { get; set; }
        private readonly YApiInteractor _apiInteractor;
        private readonly IDialogService _dialogService;
        public event Action<string>? OnImageUpdated;

        public ImageDialogViewModel(YApiInteractor apiInteractor, IDialogService dialogService)
        {
            _apiInteractor = apiInteractor;
            _dialogService = dialogService;
            ImageSource = ConfigInteractor.GetImagesLocation();
            AddButton = new(OnAddButtonClick);
            SaveCommand = new(OnSaveButtonClick);
        }

        public void LoadData()
        {
            var images = _apiInteractor.DownloadImages(ImageSource);
            foreach (var image in images)
            {
                DatabaseImagesPaths.Add(new()
                {
                    ImageName = image.Split('\\')[^1],
                    ImagePath = $"{ImageSource}\\{image}"
                });
                OnPropertyChanged(nameof(DatabaseImagesPaths));
            }
        }

        public async Task LoadDataAsync()
        {
            await foreach (var image in _apiInteractor.DownloadAllImagesAsync(ImageSource))
            {
                DatabaseImagesPaths.Add(new()
                {
                    ImageName = image.Split('\\')[^1],
                    ImagePath = $"{ImageSource}\\{image}"
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
            if (File.Exists($"{ImageSource}\\{fileName}"))
            {
                MessageBox.Show($"Файл {fileName} уже существует в папке {ImageSource}.");
                var dvm = new ReplaceImageDialogViewModel($"Заменить файл {fileName} в папке назначения?");
                bool answer = false;
                _dialogService.ShowDialog(dvm, res => answer = res);
                if (!answer)
                {
                    return;
                }
                File.Delete($"{ImageSource}\\{fileName}");
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
                File.Copy(openFileDialog.FileName, $"{ImageSource}\\{fileName}");
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
