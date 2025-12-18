

namespace PokadexApp;


public partial class SettingsPage : ContentPage
{
    StorePokemon storage = new StorePokemon();//instance of storage class to clear favourites
    bool DarkMode=Preferences.Get("Dark", false);//variable to track dark mode state
    TeamManager TeamManager = new TeamManager();
    public SettingsPage()
	{
		InitializeComponent();
        Theme.ApplyTheme(Preferences.Get("Dark", false));//apply theme based on saved preference
    }

    
    



    public void ApplyTheme(object sender, EventArgs e)
    { //method to toggle dark mode this also sets dark mode for all pages since it uses preferences which are app wide

        if (DarkMode==true)
        {
            DarkMode = false;
            Preferences.Set("Dark", true);//we update the preference to the new value of dark mode
        }
        else if(DarkMode==false)
        {
            DarkMode = true;
            Preferences.Set("Dark", false);//we update the preference to the new value of dark mode
        }

        
        Theme.ApplyTheme(Preferences.Get("Dark", false));//apply theme based on updated preference


    }
     
    public async void ViewHistory(object sender, EventArgs e)// event handler for viewing history button
    {

        await Navigation.PushModalAsync(new History());//we navigate to the history page



    }

    public async void ClearTeams(object sender, EventArgs e)// event handler for clearing teams button
    {
       TeamManager.Teams.Clear();//we clear the teams list
        await TeamManager.SaveTeams();//we save the empty list to storage
        await TeamManager.LoadTeams();//we reload the teams from storage to ensure they are cleared


    }



    public async void ViewFavourites(object sender, EventArgs e)// event handler for viewing favourites button
    {

        await Navigation.PushModalAsync(new MainPage());//we navigate to the main page which displays favourites



    }
     
    public async void ClearFavourites(object sender, EventArgs e)// event handler for clearing favourites button
    {

        storage.EraseFavourites();//we call the erase favourites method from the storage class to clear favourites



    }

}