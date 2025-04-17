using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Core.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private CitiesViewModel citiesViewModel;

        [ObservableProperty]
        private AttractionsViewModel attractionsViewModel;

        [ObservableProperty]
        private UsersViewModel usersViewModel;

        [ObservableProperty]
        private ReviewsViewModel reviewsViewModel;

        public MainViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            CitiesViewModel = new CitiesViewModel();
            AttractionsViewModel = new AttractionsViewModel();
            UsersViewModel = new UsersViewModel();
            ReviewsViewModel = new ReviewsViewModel();
        }

        public void UpdateData()
        {
            CitiesViewModel.LoadData();
            AttractionsViewModel.LoadData();
            UsersViewModel.LoadData();
            ReviewsViewModel.LoadData();
        }
    }
}
