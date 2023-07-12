using System.Collections.ObjectModel;
using System.Threading.Tasks;
using YApi;
using YConsole.Model;

namespace YConsole.ViewModels.Dialogs
{
    public class ImageDialogViewModel : ViewModelBase, IDataLoadable
    {
        public class ImageListObject
        {
            public string? ImageName { get; set; }
            public string? ImagePath { get; set; }
        }

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

        private readonly string _imageSource;
        private readonly YApiInteractor _apiInteractor;

        public ImageDialogViewModel(YApiInteractor apiInteractor)
        {
            _apiInteractor = apiInteractor;
            _imageSource = ConfigInteractor.GetImagesLocation();
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
    }
}
