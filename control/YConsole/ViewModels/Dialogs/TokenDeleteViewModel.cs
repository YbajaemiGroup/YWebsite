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

    public RelayCommand DeleteToken { get; private set; }

    private readonly YApiInteractor _apiInteractor;

    public TokenDeleteViewModel(YApiInteractor apiInteractor)
    {
        _apiInteractor = apiInteractor;
        DeleteToken = new(OnDeleteTokenClick);
    }

    private void OnDeleteTokenClick(object? ignorable)
    {
        if (TokenSource == null)
        {
            return;
        }
        StatusString = "Запрос был отправлен на сервер";
        _ = _apiInteractor.DeleteTokenFromServerAsync(TokenSource);
    }
}
