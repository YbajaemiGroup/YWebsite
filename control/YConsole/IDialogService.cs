using System;

namespace YConsole;

public interface IDialogService
{
    void ShowDialog(ViewModelBase viewModel, Action<bool> callback);
    void ShowDialog<TViewModel>(Action<bool> callback) where TViewModel : ViewModelBase;
}
