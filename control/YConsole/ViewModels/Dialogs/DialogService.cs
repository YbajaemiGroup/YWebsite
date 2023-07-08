using System;
using System.Collections.Generic;
using System.Windows;
using YConsole.Views.Dialogs;

namespace YConsole.ViewModels.Dialogs
{
    class DialogService : IDialogService
    {
        private static readonly Dictionary<Type, Type> _mappings = new();

        public static void RegisterDialog<TView, TViewModel>()
        {
            _mappings.Add(typeof(TViewModel), typeof(TView));
        }
         
        private void ShowDialogInternal(Type viewType, Type vmType, Action<bool> callback)
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
            var vm = Activator.CreateInstance(vmType);
            ((FrameworkElement)content).DataContext = vm;
            _dialogWindow.Content = content;
            _dialogWindow.ShowDialog();
        }

        public void ShowDialog(ViewModelBase viewModel, Action<bool> callback)
        {
            DialogWindow _dialogWindow = new();
            var viewType = _mappings[viewModel.GetType()];
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
            ((FrameworkElement)content).DataContext = viewModel;
            _dialogWindow.Content = content;
            _dialogWindow.ShowDialog();
        }

        public void ShowDialog<TViewModel>(Action<bool> callback) where TViewModel : ViewModelBase
        {
            var viewType = _mappings[typeof(TViewModel)];
            ShowDialogInternal(viewType, typeof(TViewModel), callback);
        }
    }
}
