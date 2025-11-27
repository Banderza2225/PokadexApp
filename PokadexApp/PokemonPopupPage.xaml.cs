using Microsoft.Maui.Controls;

namespace PokadexApp;

public partial class PokemonPopupPage : ContentPage
{
    bool darkMode = Preferences.Default.Get("Dark", false);
    SettingsPage SettingsPage = new SettingsPage();
    public PokemonPopupPage(Pokemon pokemon)
    {
        InitializeComponent();
        SettingsPage.ApplyTheme();
        
        PokemonFront.Source = pokemon.Sprites.FrontDefault;
        PokemonBack.Source = pokemon.Sprites.BackDefault;
        PokemonFrontS.Source = pokemon.Sprites.FrontShiny;
        PokemonBackS.Source = pokemon.Sprites.BackShiny;

        
        PokemonName.Text = pokemon.Name.ToUpper();
        PokemonId.Text = $"ID: {pokemon.Id}";

        
        PokemonHeight.Text = $"Height: {pokemon.Height}";
        PokemonWeight.Text = $"Weight: {pokemon.Weight}";
        PokemonBaseExp.Text = $"Base Experience: {pokemon.BaseExperience}";
    }

    void ApplyTheme()
    {
        if (darkMode == false)
        {
            Application.Current.Resources["Theme"] = Color.FromArgb("#EEEEEE");
            Application.Current.Resources["Text"] = Color.FromArgb("#000000");
            Application.Current.Resources["Accent1"] = Color.FromArgb("#DDDDDD");
        }
        else if (darkMode == true)
        {
            Application.Current.Resources["Theme"] = Color.FromArgb("#333333");
            Application.Current.Resources["Text"] = Color.FromArgb("#ffffff");
            Application.Current.Resources["Accent1"] = Color.FromArgb("#444444");
        }
    }

    private async void ClosePopup(object sender, EventArgs e)
    {
        
        await Navigation.PopModalAsync();
    }
}
