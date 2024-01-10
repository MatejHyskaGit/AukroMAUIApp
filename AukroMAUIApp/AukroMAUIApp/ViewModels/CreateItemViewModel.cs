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
    internal class CreateItemViewModel : INotifyPropertyChanged
    {
        private string _name = "";
        private string _description = "";
        private string _price = "";
        private bool _buttonenable = false;
        private float pricef = 0f;
        private string _categoryname = "";
        private AukroCategory _category;

        AukroDatabase database;

        public CreateItemViewModel()
        {
            database = new AukroDatabase();

            GoHomeCommand = new Command(GoHome);


            PriceChangeCommand = new Command(Validate);
            NameChangeCommand = new Command(Validate);

            CreateCommand = new Command(SaveToDatabase);
        }


        private void Validate()
        {
            if(float.TryParse(Price, out pricef) && Name.Length > 5)
            {
                ButtonEnable = true;
                return;
            }
            ButtonEnable = false;
        }
        private async void GoHome()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }

        public AukroCategory PickedCategory
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        public string CategoryName
        {
            get { return _categoryname; }
            set
            {
                _categoryname = value;
                OnPropertyChanged();
            }
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

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }






        private async void SaveToDatabase()
        {

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
            {
                return;
            }
            List<AukroUser> aukrouser = new List<AukroUser>();
            aukrouser = await database.GetUsersAsync();
            aukrouser = aukrouser.Where(user => user.IsLoggedIn == 1).ToList();

            AukroItem item = new AukroItem();
            item.Name = Name;
            item.Description = Description;
            item.Price = pricef;
            item.OwnerId = aukrouser.FirstOrDefault().Id;
            Name = "";
            Description = "";
            Price = "";
            item.CategoryName = PickedCategory.Name;
            await database.SaveItemAsync(item);
            await Shell.Current.GoToAsync("///MainPage");
        }



        public ICommand PriceChangeCommand { get; private set; }
        public ICommand NameChangeCommand { get; private set; }
        public ICommand GoHomeCommand { get; private set; }
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
