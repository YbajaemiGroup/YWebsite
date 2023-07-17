using System;
using YConsole.ViewModels;
using YConsole.ViewModels.Dialogs;

namespace YConsole.Utillities;

public interface IDialogService
{
    void ShowDialog(ViewModelBase viewModel, Action<bool> callback, string? questionText);
    void ShowDialog<TViewModel>(Action<bool> callback, string? questionText) where TViewModel : ViewModelBase, IDialogViewModel;
}
