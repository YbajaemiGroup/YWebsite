using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using YConsole.ViewModels;
using YConsole.ViewModels.Dialogs;
using YConsole.Views.Dialogs;

namespace YConsole.Utillities;

class DialogService : IDialogService
{
    private static readonly Dictionary<Type, Type> _mappings = new();

    public static void RegisterDialog<TView, TViewModel>()
        where TView : FrameworkElement
        where TViewModel : ViewModelBase
    {
        _mappings.Add(typeof(TViewModel), typeof(TView));
    }

    private static void ShowDialogInternal(Type viewType, ViewModelBase viewModel, Action<bool> callback)
    {
        DialogWindow _dialogWindow = new();
        EventHandler? closeEventHandler = null;
        closeEventHandler = (s, r) =>
        {
            callback(_dialogWindow.DialogResult ?? false);
            _dialogWindow.Closed -= closeEventHandler;
        };
        _dialogWindow.Closed += closeEventHandler;
        var content = Activator.CreateInstance(viewType);
        if (content == null)
        {
            throw new NullReferenceException();
        }
        var cont = (FrameworkElement)content;
        cont.DataContext = viewModel;
        _dialogWindow.Content = content;
        _dialogWindow.ShowDialog();
    }

    public void ShowDialog(ViewModelBase viewModel, Action<bool> callback, string? questionText)
    {
        var viewType = _mappings[viewModel.GetType()];
        if (viewModel is IDialogViewModel dialog && questionText != null)
        {
            dialog.SetQuestionText(questionText);
        }
        ShowDialogInternal(viewType, viewModel, callback);
    }

    public void ShowDialog<TViewModel>(Action<bool> callback, string? questionText) where TViewModel : ViewModelBase, IDialogViewModel
    {
        var viewType = _mappings[typeof(TViewModel)];
        if (typeof(TViewModel).GetConstructors().Min(c => c.GetParameters().Length) > 0)
        {
            throw new ArgumentException("TViewModel has no default constructors.");
        }
        if (Activator.CreateInstance(typeof(TViewModel)) is not ViewModelBase viewModel)
        {
            throw new ArgumentException("Can't cast TViewModel to ViewModelBase.", nameof(TViewModel));
        }
        if (viewModel is IDialogViewModel dialog && questionText != null)
        {
            dialog.SetQuestionText(questionText);
        }
        ShowDialogInternal(viewType, viewModel, callback);
    }
}
