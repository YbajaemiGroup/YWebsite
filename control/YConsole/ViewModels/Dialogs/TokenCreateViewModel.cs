using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using YApi;
using YConsole.Utillities;

namespace YConsole.ViewModels.Dialogs;

public class TokenCreateViewModel : ViewModelBase
{
    #region Bindings
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

    #endregion

    public RelayCommand CreateTokenButton { get; private set; }

    private readonly YApiInteractor _apiInteractor;

    public TokenCreateViewModel(YApiInteractor apiInteractor)
    {
        _apiInteractor = apiInteractor;
        CreateTokenButton = new(OnCreateTokenButtonClick);
    }

    private void OnCreateTokenButtonClick(object? ignorable)
    {
        StatusString = "Отправка на сервер.";
        _ = LoadToken();
    }

    private async Task LoadToken()
    {
        if (TokenSource == null)
        {
            return;
        }
        var res = _apiInteractor.LoadTokenToServerAsync(TokenSource);
        await res;
        if (res.IsCompletedSuccessfully)
        {
            StatusString = "Токен создан.";
        }
        else
        {
            StatusString = "Произошла ошибка.";
        }
        _ = Task.Run(() =>
        {
            Task.Delay(1500).Wait();
            StatusString = null;
        });
    }
}
