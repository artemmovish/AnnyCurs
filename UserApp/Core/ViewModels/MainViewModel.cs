using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Core.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        CatalogViewModel catalogVM;

        public MainViewModel()
        {
            CatalogVM = new CatalogViewModel();
        }
    }
}
