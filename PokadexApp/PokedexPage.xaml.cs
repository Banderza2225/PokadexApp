using System.Text.Json;

namespace PokadexApp;

public partial class PokedexPage : ContentPage
{
    
    StorePokemon stored = new StorePokemon();
    public PokedexPage()
    {
        InitializeComponent();
        Theme.ApplyTheme(Preferences.Get("Dark", false));
        LoadPokemonRange(1, 1000);
    }

   
    

    public  async Task   LoadPokemonRange(int start, int end)
    {
        for (int id = start; id <= end; id++)
        {
          var  p= await CreatePoke(id);
            await AddPokemon(p);
        }
    }

     public async Task<Pokemon> CreatePoke(int id)
    {
        
       
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{id}";
            HttpClient client = new HttpClient();

            var json =  await client.GetStringAsync(apiUrl);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);
            

            pokemon.Height /= 10;
            pokemon.Weight /= 10;

            return pokemon;
       
    }

    public async Task AddPokemon(Pokemon pokemon)
    {
        
        var nameLabel = new Label
        {
            Text = pokemon.Name.ToUpper(),
            FontAttributes = FontAttributes.Bold,
            FontSize = 22
        };
        nameLabel.SetDynamicResource(Label.TextColorProperty, "Text");

        var idLabel = new Label
        {
            Text = $"ID: {pokemon.Id}"
        };
        idLabel.SetDynamicResource(Label.TextColorProperty, "Text");

        
        var frame = new Frame
        {
            CornerRadius = 20,
            Margin = 10,
            Padding = 10,
            BackgroundColor = Color.FromArgb("#555555")
        };
        
        frame.Content = new HorizontalStackLayout
        {
            Spacing = 10,
            Children =
        {
            new Image
            {
                Source = pokemon.Sprites.FrontDefault,
                WidthRequest = 80,
                HeightRequest = 80
            },
            new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    nameLabel,
                    idLabel
                }
            }
        }
        };

        
        var tap = new TapGestureRecognizer();
        tap.Tapped += (s, e) => ShowPokemonPopup(pokemon);
        frame.GestureRecognizers.Add(tap);


        frame.TranslationX = 200;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            PokemonListLayout.Children.Add(frame);
        });

        await Task.WhenAll(
            frame.TranslateTo(0, 0, 120, Easing.CubicInOut)

            );




    }


    async void ShowPokemonPopup(Pokemon pokemon)
    {
        await Navigation.PushModalAsync(new PokemonPopupPage(pokemon));

        stored.SavePokemonVeiwed(pokemon);
    }


}
