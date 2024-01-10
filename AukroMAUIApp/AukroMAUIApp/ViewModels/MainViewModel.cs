using AukroMAUIApp.Database;
using AukroMAUIApp.Models;
using AukroMAUIApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace AukroMAUIApp.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _loggedinname = "";


        AukroDatabase database;

        public MainViewModel()
        {
            database = new AukroDatabase();

            GoToCreateCommand = new Command(GoCreateUser);
            GoToLoginCommand = new Command(GoLogin);
            GoDetailCommand = new Command<int>(GoDetail);
            GoItemCreateCommand = new Command(GoItem);
        }



        private async void GoCreateUser()
        {
            await Shell.Current.GoToAsync("///CreateUserPage");
        }
        private async void GoDetail(int id)
        {
            OnPropertyChanged();
            List<AukroUser> aukrouser = new List<AukroUser>();
            aukrouser = await database.GetUsersAsync();
            aukrouser = aukrouser.Where(user => user.IsLoggedIn == 1).ToList();
            if (aukrouser.FirstOrDefault() == null)
            {
                await Shell.Current.GoToAsync("///LoginPage");
                return;
            }
            await Shell.Current.GoToAsync($"///DetailsPage?id={id}");
        }
        private async void GoLogin()
        {
            OnPropertyChanged();
            List<AukroUser> aukrouser = new List<AukroUser>();
            aukrouser = await database.GetUsersAsync();
            aukrouser = aukrouser.Where(user => user.IsLoggedIn == 1).ToList();
            if (aukrouser.FirstOrDefault() == null)
            {
                await Shell.Current.GoToAsync("///LoginPage");
                return;
            }
            aukrouser.FirstOrDefault().IsLoggedIn = 0;
            await database.SaveItemAsync(aukrouser.FirstOrDefault());
            await Shell.Current.GoToAsync("///LoggedOutPage");
        }
        private async void GoItem()
        {
            OnPropertyChanged();
            List<AukroUser> aukrouser = new List<AukroUser>();
            aukrouser = await database.GetUsersAsync();
            aukrouser = aukrouser.Where(user => user.IsLoggedIn == 1).ToList();
            if (aukrouser.FirstOrDefault() == null)
            {
                await Shell.Current.GoToAsync("///LoginPage");
                return;
            }
            await Shell.Current.GoToAsync("///CreateItemPage");
        }


        public string LoggedInName
        {
            get { return _loggedinname; }
            set
            {
                _loggedinname = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoToCreateCommand { get; private set; }
        public ICommand GoToLoginCommand { get; private set; }
        public ICommand GoDetailCommand { get; private set; }
        public ICommand GoItemCreateCommand { get; private set; }



        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
