using AukroMAUIApp.Database;
using AukroMAUIApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AukroMAUIApp.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private string _name = "";
        private string _password = "";
        private string _error = "";

        AukroDatabase database;

        public LoginViewModel()
        {
            database = new AukroDatabase();


            GoToCreateCommand = new Command(GoCreateUser);
            GoHomeCommand = new Command(GoHome);
            LoginCommand = new Command(() =>
            {
                LogIn();
            });
        }

        private async void GoCreateUser()
        {
            await Shell.Current.GoToAsync("///CreateUserPage");
        }
        private async void GoHome()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }

        private async void LogIn()
        {

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorText = "Chyba. :/";
                return;
            }
            List<AukroUser> aukrouser = new List<AukroUser>();
            aukrouser = await database.GetUsersAsync();
            aukrouser = aukrouser.Where(user => user.Name == Name).ToList();
            if (aukrouser.FirstOrDefault() == null)
            {
                ErrorText = "Tento uživatel neexistuje";
                return;
            }
            if(aukrouser.FirstOrDefault().Password != Password)
            {
                ErrorText = "Nesprávné heslo";
                return;
            }
            aukrouser.FirstOrDefault().IsLoggedIn = 1;
            await database.SaveItemAsync(aukrouser.FirstOrDefault());
            Name = "";
            Password = "";
            ErrorText = "";
            await Shell.Current.GoToAsync("///MainPage");
        }




        public string ErrorText
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        public ICommand GoToCreateCommand { get; private set; }
        public ICommand GoHomeCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }


        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
