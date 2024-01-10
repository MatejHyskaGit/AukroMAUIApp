using AukroMAUIApp.Database;
using AukroMAUIApp.Models;
using SQLite;


namespace AukroMAUIApp.Views;

public partial class MainPage : ContentPage
{
    SQLiteConnection connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
    AukroDatabase database = new AukroDatabase();
    List<AukroItem> ItemList { get; set; }
    List<AukroUser> UserList { get; set; }

    public MainPage()
	{
		InitializeComponent();
	}
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        MyListView.ItemsSource = new List<AukroItem>();
        connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        ItemList = new List<AukroItem>();
        UserList = new List<AukroUser>();
        LoggedInName.Text = "";
        try
        {
            ItemList = connection.Table<AukroItem>().ToList();
            UserList = connection.Table<AukroUser>().ToList();
            AukroUser loggedin = new AukroUser();
            loggedin = UserList.Where(user => user.IsLoggedIn == 1).FirstOrDefault();
            if (loggedin != null)
            {
                LoggedInName.Text = loggedin.Name;
                LoginoutBtn.Text = "Log Out";
            }
            else
            {
                LoginoutBtn.Text = "Log In";
            }
        }
        catch (Exception)
        {
            LoginoutBtn.Text = "Log In";
        }
        


        MyListView.ItemsSource = ItemList;
        // Execute your command or logic here
        // For example, show a message when the page is opened




        /**/
    }
}

