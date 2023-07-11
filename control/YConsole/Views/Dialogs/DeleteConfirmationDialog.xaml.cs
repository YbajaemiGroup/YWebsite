using System;
using System.Windows;
using System.Windows.Controls;

namespace YConsole.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для DeleteConfirmationDialog.xaml
    /// </summary>
    public partial class DeleteConfirmationDialog : UserControl
    {
        public DeleteConfirmationDialog()
        {
            InitializeComponent();
        }

        private void OnConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            var window = Parent as Window;
            if (window == null)
            {
                throw new NullReferenceException("No window found for DeleteConfirmationDialog");
            }
            window.DialogResult = true;
            window.Close();
        }

        private void OnDeclineButtonClick(object sender, RoutedEventArgs e)
        {
            var window = Parent as Window;
            if (window == null)
            {
                throw new NullReferenceException("No window found for DeleteConfirmationDialog");
            }
            window.DialogResult = false;
            window.Close();
        }
    }
}
