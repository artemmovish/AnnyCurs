using AdminApp.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Core.ViewModels
{
    public partial class MoreInfoViewModel : ObservableObject
    {
        [ObservableProperty]
        private Attraction currentAttraction;

        [ObservableProperty]
        private AttractionImage currentImage;

        private int _currentImageIndex;

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
                OnPropertyChanged(nameof(ImageCounter));
            }
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
        private void Close()
        {
            // Закрытие окна или возврат назад
            // Например, через Messenger или сервис навигации
        }
    }
}
