

namespace PokadexApp
{
    public partial class MainPage : ContentPage
    {
        bool DarkMode = Preferences.Get("Dark",false);

        public MainPage()
        {
            InitializeComponent();
            ApplyTheme();


            
        }
        
        void ApplyTheme()
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
}
