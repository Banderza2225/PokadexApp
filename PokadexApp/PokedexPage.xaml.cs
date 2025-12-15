using System.Collections.ObjectModel;
using System.Text.Json;

namespace PokadexApp;

public partial class PokedexPage : ContentPage
{

     bool isLoading = false;
    int batchSize = 10; 
     int nextIdToLoad = 50; 
    List<Pokemon> LoadedPokemon = new List<Pokemon>();
    private CancellationTokenSource _cts = new CancellationTokenSource();

    List<string> Types = new List<string>
        {
        "normal",
        "fire",
        "water",
        "electric",
        "grass",
        "ice",
        "fighting",
        "poison",
        "ground",
        "flying",
        "psychic",
        "bug",
        "rock",
        "ghost",
        "dragon",
        "dark",
        "steel",
        "fairy"
    };

    StorePokemon stored = new StorePokemon();
    public PokedexPage()
    {
        InitializeComponent();
        TypePicker.ItemsSource = Types;
    
        Theme.ApplyTheme(Preferences.Get("Dark", false));
        LoadInitialPokemon();
        //LoadPokemonRange(1, 50);

    }

    public async Task LoadInitialPokemon() {

        _cts = new CancellationTokenSource();
        await Task.Delay(50);
         LoadPokemonRange(1, 50,_cts.Token);
        Scroll.Scrolled += OnScroll;

    }
    

    public  async Task   LoadPokemonRange(int start, int end,CancellationToken token)
    {
        for (int id = start; id <= end; id++)
        {

            if (token.IsCancellationRequested)
            {
                break;
            }

            var p = await CreatePoke(id);
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

    public async Task<Pokemon> CreatePoke(String id)
    {


        string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{id}";
        HttpClient client = new HttpClient();

        var json = await client.GetStringAsync(apiUrl);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);


        pokemon.Height /= 10;
        pokemon.Weight /= 10;

        return pokemon;

    }
    public async Task<Pokemon> CreatePokeName(String name)
    {


        string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{name}";
        HttpClient client = new HttpClient();

        var json = await client.GetStringAsync(apiUrl);

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
            FontSize = 22,
            TextColor = Colors.White
        };


        var idLabel = new Label
        {
            Text = $"ID: {pokemon.Id}",
            TextColor = Colors.White
        };



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

        //frame.SetDynamicResource(Frame.BackgroundColorProperty, "Accent1");
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


    private async void OnScroll(object sender, ScrolledEventArgs e)
    {
        double scrollY = e.ScrollY;
        double scrollViewHeight = Scroll.Height;
        double contentHeight = PokemonListLayout.Height;

        
        if (!isLoading && scrollY + scrollViewHeight + 500 >= contentHeight)
        {
            isLoading = true;
            await LoadPokemonRange(nextIdToLoad, nextIdToLoad + batchSize - 1,_cts.Token);
            nextIdToLoad += batchSize;
            isLoading = false;
        }
    }

    private async void OnSearch(object sender, EventArgs e)
    {

        _cts.Cancel();

        PokemonListLayout.Children.Clear();
        string searchText = PokemonSearchBar.Text?.Trim().ToLower();
        if (string.IsNullOrEmpty(searchText)) {

            

            LoadInitialPokemon();



            return; }
           

        PokemonListLayout.Children.Clear();

        try
        {

            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{searchText}";
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(apiUrl);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);

            
            pokemon.Height /= 10;
            pokemon.Weight /= 10;





            await AddPokemon(pokemon);
        }
        catch
        {
            await DisplayAlert("Not Found", $"No Pokémon found with name or id '{searchText}'", "OK");
        }
    }


    async void ShowPokemonPopup(Pokemon pokemon)
    {
        await Navigation.PushModalAsync(new PokemonPopupPage(pokemon));

        stored.SavePokemonVeiwed(pokemon);
    }


}
