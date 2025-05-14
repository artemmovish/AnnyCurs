using AdminApp.Core.Models;
using AdminApp.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using UserApp.Core.ViewModels.Total;

namespace UserApp.UI.Controls
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string passwordHash;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool hasError;

        public event Action RequestNavigateToRegister;
        public event Action<User> LoginSuccessful;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin);
            NavigateToRegisterCommand = new RelayCommand(OnNavigateToRegister);
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        private void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(PasswordHash))
            {
                ErrorMessage = "Пожалуйста, введите имя пользователя и пароль";
                HasError = true;
                return;
            }

            var user = DatabaseService.GetUserByLogin(Username);

            if (user == null || user.PasswordHash != PasswordHash)
            {
                ErrorMessage = "Неверное имя пользователя или пароль";
                HasError = true;
                return;
            }

            HasError = false;
            LoginSuccessful?.Invoke(user);

            SingletonViewModelHolder.Instance.User = user;
        }

        private void OnNavigateToRegister()
        {
            RequestNavigateToRegister?.Invoke();
        }
    }
}
