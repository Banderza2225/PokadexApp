using Microsoft.Maui.Controls;

namespace PokadexApp;

public partial class PokemonPopupPage : ContentPage
{
    Pokemon pokemon;
    StorePokemon stored = new StorePokemon();
    bool darkMode = Preferences.Default.Get("Dark", false);
    SettingsPage SettingsPage = new SettingsPage();
    public PokemonPopupPage(Pokemon pokemon)
    {
        this.pokemon = pokemon;
        InitializeComponent();
        Theme.ApplyTheme(Preferences.Get("Dark", false));

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

    

    private async void ClosePopup(object sender, EventArgs e)
    {
        
        await Navigation.PopModalAsync();
    }

    private void SaveToFavourites(object sender, EventArgs e)
    {
       stored.SavePokemonFavourite(pokemon);

    }
}
