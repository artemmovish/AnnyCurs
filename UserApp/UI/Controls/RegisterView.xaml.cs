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
using UserApp.Core.ViewModels;
using UserApp.Core.ViewModels.Total;

namespace UserApp.UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel)
                viewModel.Password = ((PasswordBox)sender).Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel)
                viewModel.ConfirmPassword = ((PasswordBox)sender).Password;
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

        private void ToLog_Click(object sender, RoutedEventArgs e)
        {
            // Получаем родительский TabControl (если кнопка внутри CatalogView)
            var tabControl = FindParent<TabControl>(this);
            if (tabControl != null)
            {
                tabControl.SelectedIndex = 0;
            }
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
