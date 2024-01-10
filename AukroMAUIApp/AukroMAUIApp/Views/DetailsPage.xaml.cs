using AukroMAUIApp.Database;
using AukroMAUIApp.Models;
using AukroMAUIApp.ViewModels;
using SQLite;


namespace AukroMAUIApp.Views;

public partial class DetailsPage : ContentPage
{
    SQLiteConnection connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
    List<AukroItem> ItemList { get; set; }
    List<AukroUser> UserList { get; set; }

    public DetailsPage()
	{
		InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        //MyListView.ItemsSource = new List<ContactItem>();
        connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        ItemList = new List<AukroItem>();
        ItemList = connection.Table<AukroItem>().ToList();
        UserList = new List<AukroUser>();
        UserList = connection.Table<AukroUser>().ToList();
        // Execute your command or logic here
        // For example, show a message when the page is opened

    }
}

