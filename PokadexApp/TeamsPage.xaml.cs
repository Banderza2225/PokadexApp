namespace PokadexApp;

public partial class TeamsPage : ContentPage
{
    bool DarkMode = Preferences.Get("Dark", false);
    public TeamsPage()
	{
		InitializeComponent();


        if (DarkMode == false)
        {
            Application.Current.Resources["Theme"] = Color.FromArgb("#ffffff");
            
        }
        else
        {
            Application.Current.Resources["Theme"] = Color.FromArgb("#000000");
            
        }
    }
}