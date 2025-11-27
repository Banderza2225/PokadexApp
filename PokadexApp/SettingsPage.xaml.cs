

namespace PokadexApp;

public partial class SettingsPage : ContentPage
{

	 bool DarkMode=Preferences.Get("Dark", false);
    public SettingsPage()
	{
		InitializeComponent();
        ApplyTheme();
    }

    public async void turnDark(object sender, EventArgs e)
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
    public void ApplyTheme()
    {
        if (DarkMode == false)
        {
            Application.Current.Resources["Theme"] = Color.FromArgb("#EEEEEE");
            Application.Current.Resources["Text"] = Color.FromArgb("#000000");
            Application.Current.Resources["Accent1"] = Color.FromArgb("#DDDDDD");
        }
        else if (DarkMode == true)
        {
            Application.Current.Resources["Theme"] = Color.FromArgb("#333333");
            Application.Current.Resources["Text"] = Color.FromArgb("#ffffff");
            Application.Current.Resources["Accent1"] = Color.FromArgb("#444444");
        }
    }
}