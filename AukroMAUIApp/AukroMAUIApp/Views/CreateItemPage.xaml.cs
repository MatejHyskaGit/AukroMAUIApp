using AukroMAUIApp.Database;
using AukroMAUIApp.Models;
using SQLite;


namespace AukroMAUIApp.Views;

public partial class CreateItemPage : ContentPage
{
    SQLiteConnection connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
    List<AukroItem> ItemList { get; set; }
    List<AukroUser> UserList { get; set; }
    List<AukroCategory> CategoryList { get; set; }
    AukroDatabase database;

    public CreateItemPage()
	{
        database = new AukroDatabase();
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);

        ItemList = new List<AukroItem>();
        ItemList = connection.Table<AukroItem>().ToList();

        UserList = new List<AukroUser>();
        UserList = connection.Table<AukroUser>().ToList();

        CategoryList = new List<AukroCategory>();
        CategoryList = connection.Table<AukroCategory>().ToList();
        if(CategoryList.FirstOrDefault() == null)
        {
            AukroCategory category = new AukroCategory { Name = "Gardening", Description = "Tools and items used for gardening" };
            await database.SaveItemAsync(category);
            AukroCategory category2 = new AukroCategory { Name = "Electronics", Description = "Electronic tools and other things" };
            await database.SaveItemAsync(category2);
        }
        CategoryList = connection.Table<AukroCategory>().ToList();




        CategoryPicker.ItemsSource = CategoryList;
        
        

    }
}

