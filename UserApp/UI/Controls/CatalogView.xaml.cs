﻿using System;
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

namespace UserApp.UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для CatalogView.xaml
    /// </summary>
    public partial class CatalogView : UserControl
    {
        public CatalogView()
        {
            InitializeComponent();
        }
        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {
            TypePopup.IsOpen = !TypePopup.IsOpen;

        }
        private void CityButton_Click(object sender, RoutedEventArgs e)
        {
            CityPopup.IsOpen = !CityPopup.IsOpen;
        }
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Поиск")
            {
                SearchTextBox.Text = "";
                SearchTextBox.Foreground = SystemColors.WindowTextBrush;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchTextBox.Text = "Поиск";
                SearchTextBox.Foreground = Brushes.Gray;
            }
        }
        private void SwitchToMoreInfoTab_Click(object sender, RoutedEventArgs e)
        {
            // Получаем родительский TabControl (если кнопка внутри CatalogView)
            var tabControl = FindParent<TabControl>(this);
            if (tabControl != null)
            {
                tabControl.SelectedIndex = 3;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("fdsfdsf");
        }
    }
}
