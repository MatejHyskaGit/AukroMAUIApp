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
    internal class CreateUserViewModel : INotifyPropertyChanged
    {
        private string _name = "";
        private string _password = "";
        private bool _buttonenable = false;
        private string _error = "";


        AukroDatabase database;

        public CreateUserViewModel()
        {
            database = new AukroDatabase();
            GoHomeCommand = new Command(GoHome);
            NameChangeCommand = new Command(Validate);
            PasswordChangeCommand = new Command(Validate);


            CreateCommand = new Command(async () =>
            {
                SaveToDatabase();
            });
        }

        private async void GoHome()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        private void Validate()
        {
            ErrorText = "";
            if (Name.Length > 4 && Password.Length > 4)
            {
                ButtonEnable = true;
                return;
            }
            else
            {
                ButtonEnable = false;
                return;
            }
        }

        private async void SaveToDatabase()
        {
            List<string> nameList = new List<string>();
            List<AukroUser> userList = await database.GetUsersAsync();
            foreach (var item in userList)
            {
                nameList.Add(item.Name);
            }
            Console.WriteLine(Name);

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Password) || nameList.Contains(Name))
            {
                ErrorText = "Someone with that name already exists.";
                return;
            }
            

            AukroUser user = new AukroUser();
            user.Name = Name;
            user.Password = Password;
            Name = "";
            Password = "";
            await database.SaveItemAsync(user);
            await Shell.Current.GoToAsync("///MainPage");
        }


        public bool ButtonEnable
        {
            get { return _buttonenable; }
            set
            {
                _buttonenable = value;
                OnPropertyChanged();
            }
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

        public ICommand GoHomeCommand { get; private set; }
        public ICommand NameChangeCommand { get; private set; }
        public ICommand PasswordChangeCommand { get; private set; }
        public ICommand CreateCommand { get; private set; }

        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
