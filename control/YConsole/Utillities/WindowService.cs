﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using YConsole.ViewModels;

namespace YConsole.Utillities;

class WindowService : IWindowService
{
    private static readonly Dictionary<Type, Type> _mappings = new();

    public static void RegisterView<TViewModel, TView>()
        where TViewModel : ViewModelBase
        where TView : Window
    {
        _mappings.Add(typeof(TViewModel), typeof(TView));
    }

    private readonly IServiceProvider _serviceProvider;

    public WindowService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IViewModel Show<IViewModel>() where IViewModel : ViewModelBase
    {
        var viewType = _mappings[typeof(IViewModel)];
        if (viewType == null)
        {
            throw new TypeUnloadedException($"No View setted for this IViewModel ({typeof(IViewModel).FullName}).");
        }
        var view = _serviceProvider.GetService(viewType);
        if (view == null)
        {
            throw new TypeUnloadedException($"No service loaded for {viewType}");
        }
        var windowViewModel = _serviceProvider.GetService(typeof(IViewModel));
        if (windowViewModel is null or not ViewModelBase)
        {
            throw new TypeUnloadedException($"No View setted for this IViewModel ({typeof(IViewModel).FullName}). " +
                $"Or binding does not realising ViewModelBase");
        }
        if (view is Window window)
        {
            window.DataContext = windowViewModel;
            window.Show();
            return (IViewModel)windowViewModel;
        }
        else
        {
            throw new InvalidCastException($"Cannot cast {view.GetType()} to {typeof(Window)}");
        }
    }

    public void Show(ViewModelBase viewModel)
    {
        var viewType = _mappings[viewModel.GetType()];
        if (viewType == null)
        {
            throw new TypeUnloadedException($"No View setted for this IViewModel ({viewModel.GetType()}).");
        }
        var view = _serviceProvider.GetService(viewType);
        if (view == null)
        {
            throw new TypeUnloadedException($"No service loaded for {viewType}");
        }
        if (view is Window window)
        {
            window.DataContext = viewModel;
            window.Show();
        }
        else
        {
            throw new InvalidCastException($"Cannot cast {view.GetType()} to {typeof(Window)}");
        }
    }
}
