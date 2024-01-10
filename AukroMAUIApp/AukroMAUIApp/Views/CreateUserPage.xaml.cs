using AukroMAUIApp.Database;
using AukroMAUIApp.Models;
using SQLite;


namespace AukroMAUIApp.Views;

public partial class CreateUserPage : ContentPage
{
    SQLiteConnection connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
    List<AukroUser> UserList { get; set; }

    public CreateUserPage()
	{
		InitializeComponent();
	}
    /*
    protected override void OnAppearing()
    {
        base.OnAppearing();

        //MyListView.ItemsSource = new List<ContactItem>();
        connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        UserList = new List<AukroUser>();
        UserList = connection.Table<AukroUser>().ToList();
        //MyListView.ItemsSource = DataList;
        // Execute your command or logic here
        // For example, show a message when the page is opened
    
    }*/
}

