using AdminApp.Core.Enums;
using AdminApp.Core.Models;
using AdminApp.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AdminApp.Core.ViewModels
{
    public partial class UsersViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<User> users;

        [ObservableProperty]
        private User selectedUser;

        [ObservableProperty]
        private ObservableCollection<UserType> userTypes;

        public bool IsUserSelected => SelectedUser != null;

        public UsersViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            UserTypes = new ObservableCollection<UserType>(
                System.Enum.GetValues(typeof(UserType)).Cast<UserType>());
            Users = new ObservableCollection<User>(DatabaseService.GetAllUsers());
        }

        [RelayCommand]
        private void AddUser()
        {
            //// Проверка на существующего пользователя
            //if (Users.Any(u => u.Username == "newuser"))
            //{
            //    // Обработка ошибки - пользователь уже существует
            //    return;
            //}

            //var newUser = new User
            //{
            //    Username = "newuser",
            //    PasswordHash = HashPassword("defaultpassword"), // Не забудьте реализовать хеширование
            //    Type = UserType.User,
            //    FavoriteAttractions = new List<Attraction>(),
            //    Reviews = new List<Review>()
            //};

            //try
            //{
            //    DatabaseService.AddUser(newUser);
            //    Users.Add(newUser);
            //    SelectedUser = newUser;
            //}
            //catch (Exception ex)
            //{
            //    // Обработка ошибок сохранения
            //}
        }

        [RelayCommand]
        private void DeleteUser()
        {
            if (SelectedUser == null) return;

            if (MessageBox.Show($"Delete {SelectedUser.Username}?", "Confirm",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseService.DeleteUser(SelectedUser.Id);
                Users.Remove(SelectedUser);
                SelectedUser = null;
            }
        }

        [RelayCommand]
        private void SaveUser()
        {
            if (SelectedUser == null) return;

            DatabaseService.UpdateUser(SelectedUser);
            MessageBox.Show("User saved successfully", "Success");
        }
    }
}