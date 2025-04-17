using AdminApp.Core.Models;
using AdminApp.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AdminApp.Core.ViewModels
{
    public partial class CitiesViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<City> cities;

        [ObservableProperty]
        private City selectedCity;

        public bool IsCitySelected => SelectedCity != null;

        public CitiesViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            Cities = new ObservableCollection<City>(DatabaseService.GetAllCities());
        }

        [RelayCommand]
        private void AddCity()
        {
            var newCity = new City { Name = "New City" };
            DatabaseService.AddCity(newCity);
            Cities.Add(newCity);
            SelectedCity = newCity;
        }

        [RelayCommand]
        private void DeleteCity()
        {
            if (SelectedCity == null) return;

            if (MessageBox.Show($"Delete {SelectedCity.Name}?", "Confirm",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseService.DeleteCity(SelectedCity.Id);
                Cities.Remove(SelectedCity);
                SelectedCity = null;
            }
        }

        [RelayCommand]
        private void SaveCity()
        {
            if (SelectedCity == null) return;

            DatabaseService.UpdateCity(SelectedCity);
            MessageBox.Show("City saved successfully", "Success");
        }
    }
}