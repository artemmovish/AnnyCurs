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
    /// Логика взаимодействия для MoreInfoView.xaml
    /// </summary>
    public partial class MoreInfoView : UserControl
    {
        public MoreInfoView()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем родительский TabControl (если кнопка внутри CatalogView)
            var tabControl = FindParent<TabControl>(this);
            if (tabControl != null)
            {
                tabControl.SelectedIndex = 2; 
            }
        }
        private void Button_Map_Click(object sender, RoutedEventArgs e)
        {
            if (browser.Visibility == Visibility.Visible)
            {
                // Если браузер видим, скрываем его и восстанавливаем Grid
                Grid.SetColumnSpan(ImageGrid, 2); // Или какое значение было изначально
                browser.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Если браузер скрыт, показываем его и изменяем Grid
                Grid.SetColumnSpan(ImageGrid, 1);
                browser.Visibility = Visibility.Visible;
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
