using AdminApp.Core.Enums;
using AdminApp.Core.Models;
using AdminApp.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AdminApp.Core.ViewModels
{
    public partial class AttractionsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Attraction> attractions;

        [ObservableProperty]
        private Attraction selectedAttraction;

        [ObservableProperty]
        private ObservableCollection<City> cities;

        [ObservableProperty]
        private ObservableCollection<AttractionType> attractionTypes;

        [ObservableProperty]
        private AttractionImage selectedImage;

        public bool IsAttractionSelected => SelectedAttraction != null;

        public AttractionsViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            Cities = new ObservableCollection<City>(DatabaseService.GetAllCities());
            AttractionTypes = new ObservableCollection<AttractionType>(
                System.Enum.GetValues(typeof(AttractionType)).Cast<AttractionType>());
            Attractions = new ObservableCollection<Attraction>(DatabaseService.GetAllAttractions());

            foreach (var item in Attractions)
            {
                item.Images = new ObservableCollection<AttractionImage>(item.Images);
            }
        }

        [RelayCommand]
        private void AddAttraction()
        {
            var newAttraction = new Attraction
            {
                Name = "New Attraction",
                City = Cities.FirstOrDefault(),
                Type = AttractionType.Другое,
                MapLink = "",
                Description = "",
                Images = new ObservableCollection<AttractionImage>(),
                Reviews = new ObservableCollection<Review>()
            };

            DatabaseService.AddAttraction(newAttraction);
            Attractions.Add(newAttraction);
            SelectedAttraction = newAttraction;
        }

        [RelayCommand]
        private void DeleteAttraction()
        {
            if (SelectedAttraction == null) return;

            if (MessageBox.Show($"Delete {SelectedAttraction.Name}?", "Confirm",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseService.DeleteAttraction(SelectedAttraction.Id);
                Attractions.Remove(SelectedAttraction);
                SelectedAttraction = null;
            }
        }

        [RelayCommand]
        private void SaveAttraction()
        {
            if (SelectedAttraction == null) return;

            DatabaseService.UpdateAttraction(SelectedAttraction);
            MessageBox.Show("Attraction saved successfully", "Success");
        }

        [RelayCommand]
        private void AddImage()
        {
            if (SelectedAttraction == null) return;

            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var filename in openFileDialog.FileNames)
                {
                    var newImage = new AttractionImage
                    {
                        ImagePath = filename,
                        Attraction = SelectedAttraction
                    };
                    SelectedAttraction.Images.Add(newImage);
                }
                DatabaseService.UpdateAttraction(SelectedAttraction);
            }
        }

        [RelayCommand]
        private void RemoveImage()
        {
            if (SelectedAttraction == null || SelectedImage == null) return;

            SelectedAttraction.Images.Remove(SelectedImage);
            DatabaseService.UpdateAttraction(SelectedAttraction);
        }
    }
}