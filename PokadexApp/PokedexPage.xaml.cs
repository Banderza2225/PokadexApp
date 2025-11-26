using System.Text.Json;

namespace PokadexApp;

public partial class PokedexPage : ContentPage
{
    bool DarkMode = Preferences.Get("Dark", false);

    public PokedexPage()
    {
        InitializeComponent();
        ApplyTheme();
        LoadPokemonRange(1, 1000);
    }

    void ApplyTheme()
    {
        Application.Current.Resources["Theme"] =
            DarkMode ? Color.FromArgb("#000000") : Color.FromArgb("#ffffff");
    }

    async void LoadPokemonRange(int start, int end)
    {
        for (int id = start; id <= end; id++)
        {
            await CreatePoke(id);
        }
    }

    async Task CreatePoke(int id)
    {
        try
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{id}";
            HttpClient client = new HttpClient();

            var json = await client.GetStringAsync(apiUrl);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            
            var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);

            if (pokemon == null)
            {
                await DisplayAlert("Error", $"Failed to load Pokémon {id}", "OK");
                return;
            }

            await AddPokemon(pokemon);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Exception", ex.Message, "OK");
        }
    }

    async Task AddPokemon(Pokemon pokemon)
    {
        var frame = new Frame
        {
            CornerRadius = 20,
            Margin = 10,
            Padding = 10,
            BackgroundColor = Colors.White,
            Content = new HorizontalStackLayout
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
                            new Label
                            {
                                Text = pokemon.Name.ToUpper(),
                                FontAttributes = FontAttributes.Bold,
                                FontSize = 22
                            },
                            new Label
                            {
                                Text = $"ID: {pokemon.Id}"
                            }
                        }
                    }
                }
            }
        };

        MainThread.BeginInvokeOnMainThread(() =>
        {
            PokemonListLayout.Children.Add(frame);
        });
    }
}

