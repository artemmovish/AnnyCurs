using AdminApp.Core.Models;
using AdminApp.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserApp.Core.ViewModels.Total;

namespace UserApp.Core.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        // Эти свойства будут устанавливаться вручную из code-behind
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private bool agreeToTerms;

        [ObservableProperty]
        private string errorMessage;

        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public event Action RequestNavigateToLogin;

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(OnRegister);
            NavigateToLoginCommand = new RelayCommand(() => RequestNavigateToLogin?.Invoke());
        }

        private void OnRegister()
        {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "Пожалуйста, заполните все поля.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Пароли не совпадают.";
                return;
            }

            if (!AgreeToTerms)
            {
                ErrorMessage = "Вы должны согласиться с условиями.";
                return;
            }

            // Здесь регистрация пользователя
            ErrorMessage = string.Empty;

            var user = new User()
            {
                Username = this.Username,
                PasswordHash = this.Password,
                Type = AdminApp.Core.Enums.UserType.User
            };

            DatabaseService.AddUser(user);
            SingletonViewModelHolder.Instance.User = DatabaseService.GetUserByLogin(Username);
        }
    }
}
