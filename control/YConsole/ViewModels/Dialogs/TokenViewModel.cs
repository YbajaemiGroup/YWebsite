using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YConsole.ViewModels.Dialogs
{
    public class TokenViewModel : ViewModelBase
    {

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

        public RelayCommand CreateTokenButton { get; private set; }

        public RelayCommand DeleteTokenButton { get; private set; }

        private async void CreatedToken() 
        { 
            
        }

    }
}
