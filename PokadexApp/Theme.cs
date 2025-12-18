using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokadexApp
{
    public static class Theme
    {
        public static void ApplyTheme(bool darkMode)// method to apply theme based on dark mode preference
        {
            if (darkMode == false)// if dark mode is disabled we set light theme colors
            {
                // we set the resource colors to light theme colors
                Application.Current.Resources["Theme"] = Color.FromArgb("#EEEEEE");
                Application.Current.Resources["Text"] = Color.FromArgb("#000000");
                Application.Current.Resources["Accent1"] = Color.FromArgb("#d3d3d3");
            }
            else if (darkMode == true)// if dark mode is enabled we set dark theme colors
            {

                // we set the resource colors to dark theme colors
                Application.Current.Resources["Theme"] = Color.FromArgb("#333333");
                Application.Current.Resources["Text"] = Color.FromArgb("#FFFFFF");
                Application.Current.Resources["Accent1"] = Color.FromArgb("#444444");
            }
        }
    }

}
