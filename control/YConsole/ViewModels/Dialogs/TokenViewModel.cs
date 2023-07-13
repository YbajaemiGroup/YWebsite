﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YApi;

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

        public RelayCommand CreateToken { get; private set; }
        public RelayCommand DeleteToken { get; private set; }

        private readonly YApiInteractor _apiInteractor;
        public TokenViewModel(YApiInteractor apiInteractor) 
        {
            _apiInteractor = apiInteractor;
            CreateToken = new(OnCreateTokenClick);
            DeleteToken = new(OnDeleteTokenClick);
        }

        private async void OnCreateTokenClick(object? ignorable) 
        {
            if (TokenSource == null)
            {
                return;
            }
            await _apiInteractor.LoadTokenToServerAsync(TokenSource);
        }

        private async void OnDeleteTokenClick(object? ignorable)
        {
            if (TokenSource == null) 
            { 
                return; 
            }
            await _apiInteractor.DeleteTokenFromServerAsync(TokenSource);
        }

    }
}
