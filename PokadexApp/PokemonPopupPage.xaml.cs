using Microsoft.Maui.Controls;

namespace PokadexApp;

public partial class PokemonPopupPage : ContentPage
{
    public PokemonPopupPage(Pokemon pokemon)
    {
        InitializeComponent();

        // Set the Pokémon sprites
        PokemonFront.Source = pokemon.Sprites.FrontDefault;
        PokemonBack.Source = pokemon.Sprites.BackDefault;
        PokemonFrontS.Source = pokemon.Sprites.FrontShiny;
        PokemonBackS.Source = pokemon.Sprites.BackShiny;

        // Name & ID
        PokemonName.Text = pokemon.Name.ToUpper();
        PokemonId.Text = $"ID: {pokemon.Id}";

        // Stats
        PokemonHeight.Text = $"Height: {pokemon.Height}";
        PokemonWeight.Text = $"Weight: {pokemon.Weight}";
        PokemonBaseExp.Text = $"Base Experience: {pokemon.BaseExperience}";
    }

    private async void ClosePopup(object sender, EventArgs e)
    {
        // Close the modal
        await Navigation.PopModalAsync();
    }
}
