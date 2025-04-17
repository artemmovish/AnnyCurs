using AdminApp.Core.Models;
using AdminApp.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AdminApp.Core.ViewModels
{
    public partial class ReviewsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Review> reviews;

        public ReviewsViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            // Загружаем все отзывы (в реальном приложении может потребоваться пагинация)
            var allAttractions = DatabaseService.GetAllAttractions();
            var allReviews = allAttractions
                .SelectMany(a => a.Reviews)
                .ToList();

            Reviews = new ObservableCollection<Review>(allReviews);
        }
    }
}