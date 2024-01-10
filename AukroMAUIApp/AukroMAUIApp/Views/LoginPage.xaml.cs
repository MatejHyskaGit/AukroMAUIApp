using AukroMAUIApp.Database;
using AukroMAUIApp.Models;
using SQLite;


namespace AukroMAUIApp.Views;

public partial class LoginPage : ContentPage
{
    SQLiteConnection connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
    AukroDatabase database = new AukroDatabase();
    List<AukroItem> ItemList { get; set; }
    List<AukroUser> UserList { get; set; }

    public LoginPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //MyListView.ItemsSource = new List<ContactItem>();
        connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        ItemList = new List<AukroItem>();
        UserList = new List<AukroUser>();
        //UserList = connection.Table<AukroUser>().ToList();
        //MyListView.ItemsSource = DataList;
        // Execute your command or logic here
        // For example, show a message when the page is opened

    }
}

