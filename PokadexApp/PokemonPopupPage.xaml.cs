using System.Timers;
using Microsoft.Maui.Controls;

namespace PokadexApp;

public partial class PokemonPopupPage : ContentPage
{
    Dictionary<string, int> TypeIcons = new Dictionary<string, int>
{
    { "normal",1   },
    { "fire",    10 },
    { "water",  11  },
    { "electric",13 },
    { "grass",   12 },
    { "ice",    15  },
    { "fighting", 2},
    { "poison",  4 },
    { "ground", 5  },
    { "flying", 3  },
    { "psychic",14  },
    { "bug",    7  },
    { "rock", 6    },
    { "ghost",  8  },
    { "dragon",16   },
    { "dark",    17 },
    { "steel",  9  },
    { "fairy", 10001   },
};




    Pokemon pokemon;
    StorePokemon stored = new StorePokemon();

    bool darkMode = Preferences.Default.Get("Dark", false);
    SettingsPage SettingsPage = new SettingsPage();
    public PokemonPopupPage(Pokemon pokemon)
    {
        this.pokemon = pokemon;

        //IsFavourite();
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


        foreach (var type in pokemon.Types)
        {
            var typeName = type.Type.Name.ToLower();

            if (TypeIcons.TryGetValue(typeName, out int iconUrl))
            {
                Image image = new Image
                {
                    Source = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iv/platinum/{iconUrl}.png",//iconUrl,
                    HeightRequest = 50,
                    WidthRequest = 200
                };
                

                Types.Children.Add(image);
                
                
            }
        }




    }



    private async void ClosePopup(object sender, EventArgs e)
    {

        await Navigation.PopModalAsync();
    }

    private void SaveToFavourites(object sender, EventArgs e)
    {
        List<Pokemon> favs = stored.LoadFavouritePokemon();
        if (favs.Any(p => p.Id == pokemon.Id))
        {
            stored.RemoveFavourite(pokemon);

        }
        else
        {
            stored.SavePokemonFavourite(pokemon);

        }
        //IsFavourite();

    }


    // private void IsFavourite() {

    //   List<Pokemon> favs = stored.LoadFavouritePokemon();
    //    if (favs.Any(p => p.Id == pokemon.Id))
    //    {
    //        B.Text= "Remove from Favourites";

    //    }
    //    else
    //    {
    //        B.Text = " Favourite";

    //  }



    //}
    //
}