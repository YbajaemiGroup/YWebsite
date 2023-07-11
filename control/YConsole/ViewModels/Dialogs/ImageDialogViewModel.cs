using System.Collections.ObjectModel;
using YApi;
using YConsole.Model;

namespace YConsole.ViewModels.Dialogs
{
    public class ImageDialogViewModel : ViewModelBase
    {
        public ObservableCollection<string> DatabaseImagesPaths { get; set; } = new();
        public string? SelectedImageSource { get; set; }

        private readonly string _imageSource;
        private readonly YApiInteractor _apiInteractor;

        public ImageDialogViewModel(YApiInteractor apiInteractor)
        {
            _apiInteractor = apiInteractor;
            _imageSource = ConfigInteractor.GetImagesLocation();
        }


    }
}
