using System.Windows;
using System.Windows.Controls;

namespace YConsole.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ImagesDialogWindow.xaml
    /// </summary>
    public partial class ImagesDialogWindow : Window
    {
        public ImagesDialogWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
