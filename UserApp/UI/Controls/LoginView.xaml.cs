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
using UserApp.Core.ViewModels.Total;

namespace UserApp.UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.PasswordHash = ((PasswordBox)sender).Password;
            }
        }

        private void ToReg_Click(object sender, RoutedEventArgs e)
        {
            // Получаем родительский TabControl (если кнопка внутри CatalogView)
            var tabControl = FindParent<TabControl>(this);
            if (tabControl != null)
            {
                tabControl.SelectedIndex = 1;
            }
        }
        private async void ToCatalog_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(2000);

            if (SingletonViewModelHolder.Instance.User == null) return;

            var tabControl = FindParent<TabControl>(this);
            if (tabControl != null)
            {
                tabControl.SelectedIndex = 2;
                return;
            }

            MessageBox.Show("Неверные данные");

        }

        // Вспомогательный метод для поиска родительского TabControl
        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (child != null && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }

    }
}
