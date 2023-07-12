using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YConsole.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ReplaceImageDialog.xaml
    /// </summary>
    public partial class ReplaceImageDialog : UserControl
    {
        public ReplaceImageDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = Parent as Window;
            if (window == null)
            {
                throw new NullReferenceException("No window found for DeleteConfirmationDialog");
            }
            window.DialogResult = true;
            window.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
