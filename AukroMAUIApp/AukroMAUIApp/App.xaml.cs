namespace AukroMAUIApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
	//Main - list of all items
	//CreateUser - form with creating a user, adding it into database
	//CreateItem - form with creating an item, adding the data into a database, cannot create if not logged in
	//Detail - clickable item from Main, shows details of clicked item, cannot bid if not logged in
}
