using AdminApp.Core.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Action UpdateData;
    public MainWindow()
    {
        InitializeComponent();
        var vm = (MainViewModel)DataContext;
        UpdateData = () => vm.UpdateData();
    }

    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateData();
    }
}