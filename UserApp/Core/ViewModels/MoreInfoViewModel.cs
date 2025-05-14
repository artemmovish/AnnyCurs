using AdminApp.Core.Models;
using AdminApp.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using AdminApp.Infrastructure;
using UserApp.Core.ViewModels.Total;

public partial class MoreInfoViewModel : ObservableObject
{
    // Текущий авторизованный пользователь (в реальном приложении получать из системы аутентификации)
    private User _currentUser => SingletonViewModelHolder.Instance.User;

    [ObservableProperty]
    private Attraction _currentAttraction;

    [ObservableProperty]
    private AttractionImage _currentImage;

    [ObservableProperty]
    private string _newReviewText;

    [ObservableProperty]
    private int _newReviewRating = 5;

    public ObservableCollection<int> NewReviewRatingList { get; } = new ObservableCollection<int>()
    {
        1, 2, 3, 4, 5
    };


    private int _currentImageIndex;

    public List<int> RatingStars =>
        Enumerable.Repeat(1, (int)Math.Round(CurrentAttraction?.Rating ?? 0)).ToList();

    public string ImageCounter =>
        CurrentAttraction?.Images != null ?
        $"{_currentImageIndex + 1}/{CurrentAttraction.Images.Count}" :
        "0/0";

    public void Initialize(Attraction attraction)
    {
        CurrentAttraction = attraction;

        if (attraction?.Images?.Any() == true)
        {
            CurrentImage = attraction.Images.First();
            _currentImageIndex = 0;
        }
        OnPropertyChanged(nameof(ImageCounter));
        OnPropertyChanged(nameof(RatingStars));
    }

    [RelayCommand]
    private void NextImage()
    {
        if (CurrentAttraction?.Images != null &&
            _currentImageIndex < CurrentAttraction.Images.Count - 1)
        {
            _currentImageIndex++;
            CurrentImage = CurrentAttraction.Images[_currentImageIndex];
            OnPropertyChanged(nameof(ImageCounter));
        }
    }

    [RelayCommand]
    private void PreviousImage()
    {
        if (CurrentAttraction?.Images != null && _currentImageIndex > 0)
        {
            _currentImageIndex--;
            CurrentImage = CurrentAttraction.Images[_currentImageIndex];
            OnPropertyChanged(nameof(ImageCounter));
        }
    }

    [RelayCommand]
    private void SubmitReview()
    {
        if (_currentUser == null) return;
        if (string.IsNullOrWhiteSpace(NewReviewText)) return;

        var newReview = new Review
        {
            Text = NewReviewText,
            Rating = Convert.ToInt32(NewReviewRating),
            Author = _currentUser,
            Attraction = CurrentAttraction,
            CreatedDate = DateTime.Now
        };

        // Добавляем в локальное хранилище
        DatabaseService.AddReview(newReview);

        // Добавляем в коллекцию отзывов достопримечательности
        CurrentAttraction.Reviews.Add(newReview);

        // Сбрасываем форму
        NewReviewText = string.Empty;
        NewReviewRating = 5;

        // Обновляем отображение рейтинга
        OnPropertyChanged(nameof(RatingStars));
    }

    [RelayCommand]
    private void Close()
    {
        // В реальном приложении здесь будет закрытие окна
        // Например: App.NavigationService.GoBack();
    }
}