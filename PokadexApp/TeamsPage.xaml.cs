namespace PokadexApp;

public partial class TeamsPage : ContentPage
{
    bool DarkMode = Preferences.Get("Dark", false);
    public TeamsPage()
	{
		InitializeComponent();


        Theme.ApplyTheme(Preferences.Get("Dark", false));
    }

    public void AddTeam(object sender ,EventArgs e ) { 
    
    
    }
}