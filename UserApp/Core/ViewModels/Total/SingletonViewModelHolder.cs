using AdminApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.UI.Controls;

namespace UserApp.Core.ViewModels.Total
{
    public sealed class SingletonViewModelHolder
    {
        // Ленивая инициализация (потокобезопасная)
        private static readonly Lazy<SingletonViewModelHolder> _instance =
            new Lazy<SingletonViewModelHolder>(() => new SingletonViewModelHolder());

        public static SingletonViewModelHolder Instance => _instance.Value;

        public CatalogViewModel CatalogViewModel { get; }
        public MoreInfoViewModel MoreInfoViewModel { get; }
        public LoginViewModel LoginViewModel { get; }
        public RegisterViewModel RegisterViewModel { get; }


        public User? User { get; set; }
        private SingletonViewModelHolder()
        {
            CatalogViewModel = new CatalogViewModel();
            MoreInfoViewModel = new MoreInfoViewModel();
            LoginViewModel = new LoginViewModel();
            RegisterViewModel = new RegisterViewModel();
            User = null;
        }

        public void SetAttraction(Attraction attraction)
        {
            MoreInfoViewModel.Initialize(attraction);
        }
    }
}
