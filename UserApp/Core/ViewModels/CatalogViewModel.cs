using AdminApp.Core.Enums;
using AdminApp.Core.Models;
using AdminApp.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UserApp.Core.ViewModels.Total;

namespace UserApp.Core.ViewModels
{
    public partial class CatalogViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Attraction> attractions;
        [ObservableProperty]
        ObservableCollection<Attraction> attractionsSort;

        [ObservableProperty]
        ObservableCollection<City> cities;
        [ObservableProperty]
        City filtrCiry;

        [ObservableProperty]
        ObservableCollection<AttractionType> types;
        [ObservableProperty]
        AttractionType filtrType;

        [ObservableProperty]
        ObservableCollection<string> filtrs;

        [ObservableProperty]
        string searchBox = "Поиск";
        public CatalogViewModel()
        {
            LoadData();
        }

        async void LoadData()
        {
            Attractions = new ObservableCollection<Attraction>(DatabaseService.GetAllAttractions());
            Cities = new ObservableCollection<City>(DatabaseService.GetAllCities());
            Filtrs = new ObservableCollection<string>();
            Types = new ObservableCollection<AttractionType>()
            {
                AttractionType.Архитектура,
                AttractionType.Парк,
                AttractionType.Монумент,
                AttractionType.Природа,
                AttractionType.Музей,
                AttractionType.Другое
            };
            AttractionsSort = new ObservableCollection<Attraction>(Attractions);
        }
        [RelayCommand]
        void ResetFilters()
        {
            // Сбрасываем поисковые фильтры
            Filtrs.Clear();

            // Сбрасываем город
            FiltrCiry = null;

            // Сбрасываем тип (устанавливаем значение по умолчанию)
            FiltrType = default;

            // Сбрасываем текст поиска
            SearchBox = "Поиск";

            // Показываем все достопримечательности без фильтров
            AttractionsSort = new ObservableCollection<Attraction>(Attractions);
        } 
        void ApplyFilters()
        {
            var filtered = Attractions.AsEnumerable();

            // Apply search filters (Name and Description)
            if (Filtrs.Any())
            {
                filtered = filtered.Where(attraction =>
                    Filtrs.Any(filter =>
                        attraction.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                        attraction.Description.Contains(filter, StringComparison.OrdinalIgnoreCase)
                    )
                );
            }

            // Apply city filter
            if (FiltrCiry != null)
            {
                filtered = filtered.Where(attraction => attraction.City == FiltrCiry);
            }

            // Apply type filter
            if (FiltrType != default)
            {
                filtered = filtered.Where(attraction => attraction.Type == FiltrType);
            }

            AttractionsSort = new ObservableCollection<Attraction>(filtered);
        }
        [RelayCommand]
        void Search()
        {
            if (SearchBox == "Поиск") return;
            Filtrs.Add(SearchBox);
            SearchBox = "Поиск";
            ApplyFilters();
        }

        [RelayCommand]
        void Type(AttractionType type)
        {
            FiltrType = default;
            FiltrType = type;
            ApplyFilters();
        }
        [RelayCommand]
        void City(City city)
        {
            FiltrCiry = city;
            ApplyFilters();
        }

        [RelayCommand]
        void ShowDetails(Attraction attraction)
        {
            SingletonViewModelHolder.Instance.SetAttraction(attraction);
        }
        [RelayCommand]
        void RemoveFilter(string item)
        {
            Filtrs.Remove(item);
            ApplyFilters();
        }
    }
}
