

namespace PokadexApp;

public partial class SettingsPage : ContentPage
{

	 bool DarkMode=Preferences.Get("Dark", false);
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
        else
        {
            DarkMode = true;
            Preferences.Set("Dark", false);
        }



        Theme.ApplyTheme(Preferences.Get("Dark", false));


    }

}