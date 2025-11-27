

namespace PokadexApp;

public partial class SettingsPage : ContentPage
{

	 bool DarkMode=Preferences.Get("Dark", false);
    public SettingsPage()
	{
		InitializeComponent();
        ApplyTheme();
    }

    private async void turnDark(object sender, EventArgs e)
    {
        if (DarkMode == false)
        {
           
            DarkMode = true;
            Preferences.Set("Dark", true);
            darkB.Text = "Dark Mode";
        }
        else if(DarkMode == true ) {
            
            DarkMode = false;
            Preferences.Set("Dark", false);
            darkB.Text = "Light Mode";
        }


        ApplyTheme();

        
    }
    async void ApplyTheme()
    {
        if (DarkMode == false)
        {
            Application.Current.Resources["Theme"] = Color.FromArgb("#ffffff");
        }
        else if (DarkMode == true)
        {
            Application.Current.Resources["Theme"] = Color.FromArgb("#000000");
        }
       
    }
}