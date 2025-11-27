using System.Text.Json;

namespace PokadexApp;

public partial class PokedexPage : ContentPage
{
    bool DarkMode = Preferences.Get("Dark", false);
    string TextColor, FrameColor;
    Color text;
    SettingsPage SettingsPage = new SettingsPage();
    public PokedexPage()
    {
        InitializeComponent();
        SettingsPage.ApplyTheme();
        LoadPokemonRange(1, 1000);
    }

   


    async void LoadPokemonRange(int start, int end)
    {
        for (int id = start; id <= end; id++)
        {
           Pokemon p= await CreatePoke(id);
            await AddPokemon(p);
        }
    }

     async Task<Pokemon> CreatePoke(int id)
    {
        
       
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{id}";
            HttpClient client = new HttpClient();

            var json =  await client.GetStringAsync(apiUrl);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);
            

            // Convert height and weight to human-readable units
            pokemon.Height /= 10;
            pokemon.Weight /= 10;

            return pokemon;
       
    }

    async Task AddPokemon(Pokemon pokemon)
    {
        // Labels
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

        // Frame
        var frame = new Frame
        {
            CornerRadius = 20,
            Margin = 10,
            Padding = 10,
            BackgroundColor = Color.FromArgb("#555555")
        };
        //frame.SetDynamicResource(Frame.BackgroundColorProperty, "Theme");

        // Frame content
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

        // Tap gesture for popup
        var tap = new TapGestureRecognizer();
        tap.Tapped += (s, e) => ShowPokemonPopup(pokemon);
        frame.GestureRecognizers.Add(tap);

        // Add frame to layout
        MainThread.BeginInvokeOnMainThread(() =>
        {
            PokemonListLayout.Children.Add(frame);
        });

       
    }


    async void ShowPokemonPopup(Pokemon pokemon)
    {
        await Navigation.PushModalAsync(new PokemonPopupPage(pokemon));


    }


}
