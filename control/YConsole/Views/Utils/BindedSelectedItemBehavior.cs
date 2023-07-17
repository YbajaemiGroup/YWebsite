using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace YConsole.Views.Utils;

public class BindedSelectedItemBehavior : Behavior<TreeView>
{
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(BindedSelectedItemBehavior), new UIPropertyMetadata(null, OnSelectedItemChanged));

    private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is TreeViewItem item)
        {
            item.SetValue(TreeViewItem.IsSelectedProperty, true);
        }
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.SelectedItemChanged += OnTreeViewItemChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject != null)
        {
            AssociatedObject.SelectedItemChanged -= OnTreeViewItemChanged;
        }
    }

    private void OnTreeViewItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        SelectedItem = e.NewValue;
    }
}
