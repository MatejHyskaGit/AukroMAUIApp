using AukroMAUIApp.Database;
using AukroMAUIApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace AukroMAUIApp.ViewModels
{
    internal class DetailsViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _description;
        private string _price;
        private bool _buttonenable;
        private float newpricef;
        private float pricef;
        private string _ownername;
        private string _newprice;
        private string _lastbiddername;
        private bool _deletevisible;
        private string _categoryname;

        SQLiteConnection connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);

        AukroDatabase database;

        List<AukroItem> aukroitem = new List<AukroItem>();
        List<AukroUser> aukrouser = new List<AukroUser>();
        List<AukroUser> aukrobidder = new List<AukroUser>();
        

        public DetailsViewModel()
        {
            database = new AukroDatabase();

            BidCommand = new Command(Bid);
            PriceChangeCommand = new Command(Validate);

            GoHomeCommand = new Command(GoHome);
            DeleteCommand = new Command(Delete);
        }


        private async void Bid()
        {
            List<AukroUser> aukrouser = new List<AukroUser>();
            List<AukroUser> owner = new List<AukroUser>();
            aukrouser = await database.GetUsersAsync();
            aukrouser = aukrouser.Where(user => user.IsLoggedIn == 1).ToList();
            owner = connection.Table<AukroUser>().ToList();
            owner = owner.Where(user => user.Id == aukroitem.FirstOrDefault().OwnerId).ToList();
            if (aukrobidder.FirstOrDefault() == null || aukrouser.FirstOrDefault().Id != aukrobidder.FirstOrDefault().Id)
            {
                if (aukrouser.FirstOrDefault().Id != owner.FirstOrDefault().Id)
                {
                    aukroitem.FirstOrDefault().LastBidderId = aukrouser.FirstOrDefault().Id;
                    Price = NewPrice;
                    aukroitem.FirstOrDefault().Price = newpricef;
                    LastBidderName = aukrouser.FirstOrDefault().Name;
                    await database.SaveItemAsync(aukroitem.FirstOrDefault());
                    NewPrice = "";
                    await Shell.Current.GoToAsync("///MainPage");
                }
            }

            
        }
        private async void Delete()
        {
            List<AukroItem> ItemList = new List<AukroItem>();
            AukroItem Item = new AukroItem();
            ItemList = await database.GetItemsAsync();
            Item = ItemList.Where(i => i.Id == Id).FirstOrDefault();
            await database.DeleteItemAsync(Item);
            await Shell.Current.GoToAsync("///MainPage");
        }

        private void Validate()
        {
            float.TryParse(Price, out pricef);
            if (float.TryParse(NewPrice, out newpricef) && newpricef > pricef)
            {
                ButtonEnable = true;
                return;
            }
            ButtonEnable = false;
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

        public bool DeleteVisible
        {
            get { return _deletevisible; }
            set
            {
                _deletevisible = value;
                OnPropertyChanged();
            }
        }

        public string LastBidderName
        {
            get { return _lastbiddername; }
            set
            {
                _lastbiddername = value;
                OnPropertyChanged();
            }
        }

        public string NewPrice
        {
            get { return _newprice; }
            set
            {
                _newprice = value;
                OnPropertyChanged();
            }
        }

        public string OwnerName
        {
            get { return _ownername; }
            set
            {
                _ownername = value;
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

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
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

        private async void GoHome()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string id = HttpUtility.UrlDecode(query["id"].ToString());
            int res;
            bool result = Int32.TryParse(id, out res);
            Id = res;
            aukroitem = connection.Table<AukroItem>().ToList();
            aukroitem = aukroitem.Where(item => item.Id == _id).ToList();
            Name = aukroitem.FirstOrDefault().Name;
            Description = aukroitem.FirstOrDefault().Description;
            Price = aukroitem.FirstOrDefault().Price.ToString();
            aukrouser = connection.Table<AukroUser>().ToList();
            aukrouser = aukrouser.Where(user => user.Id == aukroitem.FirstOrDefault().OwnerId).ToList();
            OwnerName = aukrouser.FirstOrDefault().Name;
            DeleteVisible = false;

            NewPrice = "";
            LastBidderName = "";
            aukrobidder = connection.Table<AukroUser>().ToList();
            aukrobidder = aukrobidder.Where(user => user.Id == aukroitem.FirstOrDefault().LastBidderId).ToList();
            if (aukrobidder.FirstOrDefault() != null)
            {
                LastBidderName = aukrobidder.FirstOrDefault().Name;
            }

            List<AukroUser> loggedinuser = new List<AukroUser>();
            loggedinuser = connection.Table<AukroUser>().ToList();
            loggedinuser = loggedinuser.Where(user => user.IsLoggedIn == 1).ToList();

            if (loggedinuser.FirstOrDefault().Id == aukrouser.FirstOrDefault().Id)
            {
                DeleteVisible = true;
            }

            CategoryName = aukroitem.FirstOrDefault().CategoryName;


        }
        public ICommand DeleteCommand { get; private set; }
        public ICommand BidCommand { get; private set; }
        public ICommand PriceChangeCommand { get; private set; }
        public ICommand GoHomeCommand { get; private set; }


        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
