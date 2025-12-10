

namespace PokadexApp;


public partial class SettingsPage : ContentPage
{
    StorePokemon storage = new StorePokemon();
    bool DarkMode=Preferences.Get("Dark", false);
    TeamManager TeamManager = new TeamManager();
    public SettingsPage()
	{
		InitializeComponent();
        Theme.ApplyTheme(Preferences.Get("Dark", false));
    }

    
    



    public void ApplyTheme(object sender, EventArgs e) { 
    
       if (DarkMode==true)
        {
            DarkMode = false;
            Preferences.Set("Dark", true);
        }
        else if(DarkMode==false)
        {
            DarkMode = true;
            Preferences.Set("Dark", false);
        }



        Theme.ApplyTheme(Preferences.Get("Dark", false));


    }
     
    public async void ViewHistory(object sender, EventArgs e)
    {

        await Navigation.PushModalAsync(new History());

        

    }

    public async void ClearTeams(object sender, EventArgs e)
   {
       TeamManager.Teams.Clear();
      await TeamManager.SaveTeams();
       await TeamManager.LoadTeams();


    }



    public async void ViewFavourites(object sender, EventArgs e)
    {

        await Navigation.PushModalAsync(new MainPage());



    }
     
    public async void ClearFavourites(object sender, EventArgs e)
    {

        storage.EraseFavourites();



    }

}