using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace YConsole.Views.Utils
{
    public class BindablePlayerDescriptorsBehaviour : Behavior<ComboBox>
    {
        public int? RoundDescriptor
        {
            get => (int?)GetValue(RoundDescriptorProperty);
            set => SetValue(RoundDescriptorProperty, value);
        }

        public int? PlayerDescriptor
        {
            get => (int?)GetValue(PlayerDescriptorProperty);
            set => SetValue(PlayerDescriptorProperty, value);
        }

        public bool? IsUpper
        {
            get => (bool?)GetValue(IsUpperProperty);
            set => SetValue(IsUpperProperty, value);
        }

        public IPlayerMetadataCommand? PlayerMetadataCommand
        {
            get => (IPlayerMetadataCommand?)GetValue(PlayerMetadataCommandProperty);
            set => SetValue(PlayerMetadataCommandProperty, value);
        }

        public static readonly DependencyProperty RoundDescriptorProperty =
            DependencyProperty.Register("RoundDescriptor", typeof(int), typeof(BindablePlayerDescriptorsBehaviour), new UIPropertyMetadata(null));

        public static readonly DependencyProperty PlayerDescriptorProperty =
            DependencyProperty.Register("PlayerDescriptor", typeof(int), typeof(BindablePlayerDescriptorsBehaviour), new UIPropertyMetadata(null));

        public static readonly DependencyProperty PlayerMetadataCommandProperty =
            DependencyProperty.Register("PlayerMetadataCommand", typeof(IPlayerMetadataCommand), typeof(BindablePlayerDescriptorsBehaviour), new(null));

        public static readonly DependencyProperty IsUpperProperty =
            DependencyProperty.Register("IsUpper", typeof(bool), typeof(BindablePlayerDescriptorsBehaviour), new(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnSelectionChanged;
            if (PlayerMetadataCommand != null)
            {
                PlayerMetadataCommand.DataUpdated += OnDataUpdated;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
            if (PlayerMetadataCommand != null)
            {
                PlayerMetadataCommand.DataUpdated -= OnDataUpdated;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlayerMetadataCommand == null || PlayerDescriptor == null || RoundDescriptor == null || IsUpper == null)
            {
                return;
            }
            PlayerMetadataCommand.Execute(PlayerDescriptor.Value, RoundDescriptor.Value, IsUpper.Value, AssociatedObject.SelectedItem);
        }

        private void OnDataUpdated(int playerDescriptor, int roundDescriptor, bool isUpper, object value)
        {
            if (PlayerDescriptor != playerDescriptor || RoundDescriptor != roundDescriptor)
            {
                return;
            }
            AssociatedObject.SelectedItem = value;
        }
    }
}
