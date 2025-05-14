using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Core.ViewModels.Total;
using UserApp.UI.Controls;

namespace UserApp.Core.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        CatalogViewModel catalogVM;

        [ObservableProperty]
        MoreInfoViewModel moreInfoVM;
        
        [ObservableProperty]
        LoginViewModel loginVM;

        [ObservableProperty]
        RegisterViewModel registerVM;

        public MainViewModel()
        {
            CatalogVM = SingletonViewModelHolder.Instance.CatalogViewModel;
            MoreInfoVM = SingletonViewModelHolder.Instance.MoreInfoViewModel;
            LoginVM = SingletonViewModelHolder.Instance.LoginViewModel;
            RegisterVM = SingletonViewModelHolder.Instance.RegisterViewModel;
        }
    }
}
