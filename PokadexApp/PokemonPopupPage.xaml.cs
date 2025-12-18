using System.Timers;
using Microsoft.Maui.Controls;

namespace PokadexApp;

public partial class PokemonPopupPage : ContentPage
{
    Dictionary<string, int> TypeIcons = new Dictionary<string, int>// Mapping of type names to icon IDs
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




    Pokemon pokemon;//the pokmeon to display
    StorePokemon stored = new StorePokemon();//instance of the storage class to save to favourites

    bool darkMode = Preferences.Default.Get("Dark", false);
    SettingsPage SettingsPage = new SettingsPage();//instance of settings page to access theme settings
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
        //we set the sources of the images to the sprites from the pokemon object passed in

        PokemonName.Text = pokemon.Name.ToUpper();
        PokemonId.Text = $"ID: {pokemon.Id}";
        //we set the text of the labels to the corresponding data from the pokemon object

        PokemonHeight.Text = $"Height: {pokemon.Height}";
        PokemonWeight.Text = $"Weight: {pokemon.Weight}";
        PokemonBaseExp.Text = $"Base Experience: {pokemon.BaseExperience}";
        //we set the text of the labels to the corresponding data from the pokemon object
        StatsDisplay();

        foreach (var type in pokemon.Types)//each type the pokemon has
        {
            var typeName = type.Type.Name.ToLower();

            if (TypeIcons.TryGetValue(typeName, out int iconUrl)) // we get type of the pokmon fromn the object the get what iconurl number it corresponds to using the dictionary
            {
                Image image = new Image//we create a new image
                {
                    Source = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/types/generation-iv/platinum/{iconUrl}.png",// we set the source to the corresponding icon url
                    HeightRequest = 50,
                    WidthRequest = 200
                };
                

                Types.Children.Add(image);//we add the image to the types layout


            }
            //we add the image to the types layout fro each type the pokemon has
        }




    }



    private async void ClosePopup(object sender, EventArgs e)//closes the popup 
    {

        await Navigation.PopModalAsync();//we pop the modal page off the navigation stack to close the popup
    }

    private void SaveToFavourites(object sender, EventArgs e)
    {
        List<Pokemon> favs = stored.LoadFavouritePokemon();//so here we load the favourite pokemons from storage into a list 
        if (favs.Any(p => p.Id == pokemon.Id))//then we check if this pokemon is already in the favourites list by checking if any pokemon in the list has the same id as the current pokemon
        {
            stored.RemoveFavourite(pokemon);//if it is we remove it from the favourites

        }
        else
        {
            stored.SavePokemonFavourite(pokemon);//if it isnt we add it to the favourites

        }
        

    }

    private void StatsDisplay()
    {


        var layout = new VerticalStackLayout {  };
        layout.Children.Add(CreateStatBar("HP", pokemon.Stats.First(s => s.Stat.Name == "hp").BaseStat, 255));
        layout.Children.Add(CreateStatBar("Attack", pokemon.Stats.First(s => s.Stat.Name == "attack").BaseStat, 190));
        layout.Children.Add(CreateStatBar("Defense", pokemon.Stats.First(s => s.Stat.Name == "defense").BaseStat, 250));
        layout.Children.Add(CreateStatBar("Sp. Atk", pokemon.Stats.First(s => s.Stat.Name == "special-attack").BaseStat, 194));
        layout.Children.Add(CreateStatBar("Sp. Def", pokemon.Stats.First(s => s.Stat.Name == "special-defense").BaseStat, 250));
        layout.Children.Add(CreateStatBar("Speed", pokemon.Stats.First(s => s.Stat.Name == "speed").BaseStat, 180));
        //we create a vertical stack layout and add stat bars for each stat of the pokemon using the CreateStatBar method
        Frame2.Children.Add(layout);//we add the layout to the Frame2 in the xaml
    }


    private VerticalStackLayout CreateStatBar(string statName, int value, int maxValue)//method to create a stat bar
    {

        double percentage = (double)value / maxValue;//we calculate the percentage of the stat value compared to the max value

        var barLayout = new VerticalStackLayout { Spacing = 2 };//we create a vertical stack layout for the stat bar


        barLayout.Children.Add(new Label//we add a label to display the stat name and value
        {
            Text = $"{statName}: {value}",
            TextColor = Colors.White,
            FontSize = 14
        });


        var barBackground = new BoxView//we create a boxview for the background of the stat bar
        {
            Color = Colors.Gray,
            HeightRequest = 10,
            CornerRadius = 5,
            WidthRequest = 200
        };

        var barForeground = new BoxView//we create a boxview for the foreground of the stat bar
        {
            Color = Colors.LimeGreen,
            HeightRequest = 10,
            CornerRadius = 5,
            WidthRequest = 200 * percentage
        };


        var grid = new Grid { ColumnSpacing = 0 };//we create a grid to hold the background and foreground boxviews
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        grid.Children.Add(barBackground);//we add the background boxview to the grid
        grid.Children.Add(barForeground);//we add the foreground boxview to the grid

        barLayout.Children.Add(grid);//we add the grid to the stat bar layout

        barLayout.HorizontalOptions = LayoutOptions.Center;
        return barLayout; //we return the stat bar layout
    }
    private async void  OpenTeamsSelection(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new TeamSelection( pokemon));//we open the team selection page and pass in the current pokemon so we can add it to a elected team

        stored.SavePokemonVeiwed(pokemon);//we save the pokemon as viewed in storage


    }
}