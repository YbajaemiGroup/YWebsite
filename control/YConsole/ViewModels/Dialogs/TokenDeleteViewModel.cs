using System.Threading.Tasks;
using YApi;

namespace YConsole.ViewModels.Dialogs;

public class TokenDeleteViewModel : ViewModelBase
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

    public RelayCommand DeleteTokenButton { get; private set; }

    private readonly IApiInteractor _apiInteractor;

    public TokenDeleteViewModel(IApiInteractor apiInteractor)
    {
        _apiInteractor = apiInteractor;
        DeleteTokenButton = new(OnDeleteTokenClick);
    }

    private void OnDeleteTokenClick(object? ignorable)
    {
        StatusString = "Отправка на сервер.";
        _ = DeleteToken();
    }

    private async Task DeleteToken()
    {
        if (TokenSource == null)
        {
            return;
        }
        var res = _apiInteractor.DeleteTokenFromServerAsync(TokenSource);
        await res;
        if (res.IsCompletedSuccessfully)
        {
            StatusString = "Токен удален.";
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
