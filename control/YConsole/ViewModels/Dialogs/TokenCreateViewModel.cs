using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YApi;

namespace YConsole.ViewModels.Dialogs
{
    public class TokenCreateViewModel : ViewModelBase
    {
        private string? statusString;

        public string? StatusString
        {
            get { return statusString; }
            set 
            { 
                statusString = value;
                OnPropertyChanged(nameof(StatusString));
            }
        }


        private string? tokenSource;

        public string? TokenSource
        {
            get { return tokenSource; }
            set
            {
                tokenSource = value;
                OnPropertyChanged(nameof(TokenSource));
            }
        }

        public RelayCommand CreateToken { get; private set; }

        private readonly YApiInteractor _apiInteractor;

        public TokenCreateViewModel(YApiInteractor apiInteractor)
        {
            _apiInteractor = apiInteractor;
            CreateToken = new(OnCreateTokenClick);
        }

        private void OnCreateTokenClick(object? ignorable)
        {
            if (TokenSource == null)
            {
                return;
            }
            StatusString = "Запрос был отправлен на сервер";
            _ = _apiInteractor.LoadTokenToServerAsync(TokenSource);
        }

    }
}
