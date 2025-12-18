using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace PokadexApp;

public partial class PokedexPage : ContentPage
{

     bool isLoading = false;// flag to prevent multiple simultaneous loads
    int batchSize = 10; // number of pokemon to load in each batch
    int nextIdToLoad = 50; // next pokemon ID to load
  
    private CancellationTokenSource _cts = new CancellationTokenSource();/* now this for cancelling ongoing loading tasks when a new search or filter is applied this was because everytime u searchewd something 


    or applied a filter it would continue loading the previous list of pokemon causing a mix of results so now with this cancellation token source we can cancel any ongoing loading tasks before starting a new one*/

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
    };// list of pokemon types for filtering this will all be added to a picker in the UI

    StorePokemon stored = new StorePokemon();// we did this so that any pokemon that veiwedgets added to storage as veiwed pokemon
    public PokedexPage()
    {
        InitializeComponent();
        TypePicker.ItemsSource = Types;// setting the item source of the type picker to the list of types defined above

        Theme.ApplyTheme(Preferences.Get("Dark", false));// applying theme based on user preference check the Theme class for more details
        LoadInitialPokemon();// loading initial batch of pokemon when the page is initialized
        //LoadPokemonRange(1, 50);

    }

    public async Task LoadInitialPokemon() {
        /* here we create a new cancelation token a pass it into the loadpokemonrange so it can be cancelled any time by another method  */
        _cts = new CancellationTokenSource();
        await Task.Delay(50);
         LoadPokemonRange(1, 50,_cts.Token);
        Scroll.Scrolled += OnScroll;

    }
    

    public  async Task   LoadPokemonRange(int start, int end,CancellationToken token)//* this method loads a range of pokemon from the pokeapi based on the start and end id provided it also takes a cancellation token as a parameter to allow cancelling ongoing loading tasks when a new search or filter is applied*/
    {
        for (int id = start; id <= end; id++)
        {

            if (token.IsCancellationRequested)// check if cancellation has been requested if so we break out of the loop and stop loading more pokemon
            {
                break;// exit the loop if cancellation is requested
            }

            var p = await CreatePoke(id);// create the pokemon object by calling the CreatePoke method with the current id
            await AddPokemon(p);// add the created pokemon to the UI by calling the AddPokemon method

        }
    }

     public async Task<Pokemon> CreatePoke(int id)
    {
        /*here we input a int for id the use pokemon api to retreive it from the internet but we recieve it as a json file so we deserialize it into a pokmon object look at the poke
         mon class for more details how each feild in the json object is mapped to the pokemon feilds */
       
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{id}";// constructing the api url using the provided id
        HttpClient client = new HttpClient();// creating a new http client to make the request

        var json =  await client.GetStringAsync(apiUrl);// making an asynchronous request to get the json string from the api

        var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);// 
            

            pokemon.Height /= 10;// here im converting the height and weight from decimetres and hectograms to metres and kilograms respectively by dividing by 10
        pokemon.Weight /= 10;

            return pokemon;// returning the created pokemon object

    }

    public async Task<Pokemon> CreatePoke(String id) //essentially the same method as above but taking a string as input this is used for searching by name this ive used method overloading to allow for searching between id or string 
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
    
    public async Task AddPokemon(Pokemon pokemon)//this method is responsible for adding a pokemon to the UI it creates the necessary UI elements and adds them to the layout also adds a tap gesture recognizer to show a popup with more details when tapped
    {

        //similar to the method in the history and main page but adapted for pokedex page
        var nameLabel = new Label// creating a label for the pokemon name
        {
            Text = pokemon.Name.ToUpper(),
            FontAttributes = FontAttributes.Bold,
            FontSize = 22,
            TextColor = Colors.White

            //
        };


        var idLabel = new Label// creating a label for the pokemon id
        {
            Text = $"ID: {pokemon.Id}",
            TextColor = Colors.White
        };



        var frame = new Frame// creating a frame to hold the pokemon info
        {
            CornerRadius = 20,
            Margin = 10,
            Padding = 10,
            BackgroundColor = Color.FromArgb("#555555")
        };

        frame.Content = new HorizontalStackLayout// creating a horizontal stack layout to arrange the image and labels horizontally
        {
            Spacing = 10,
            Children =
        {
            new Image// creating an image to display the pokemon sprite
            {
                Source = pokemon.Sprites.FrontDefault,
                WidthRequest = 80,
                HeightRequest = 80
            },
            new VerticalStackLayout// creating a vertical stack layout to arrange the labels vertically
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
        var tap = new TapGestureRecognizer();// creating a tap gesture recognizer to handle taps on the frame
        tap.Tapped += (s, e) => ShowPokemonPopup(pokemon);//    when tapped, show the pokemon popup with more details
        frame.GestureRecognizers.Add(tap);


        frame.TranslationX = 200;// setting the initial translation of the frame for animation

        MainThread.BeginInvokeOnMainThread(() =>
        {
            PokemonListLayout.Children.Add(frame);// adding the frame to the pokemon list layout on the main thread
        });

        await Task.WhenAll(
            frame.TranslateTo(0, 0, 120, Easing.CubicInOut)//   animate the frame into view

            );


    }


    private async void OnScroll(object sender, ScrolledEventArgs e)//i needed to only load pokemon when u need to see them so when u scroll this method is called it checks if the user has scrolled near the bottom of the list if so it loads the next batch of pokemon
    {
        double scrollY = e.ScrollY;// current scroll position
        double scrollViewHeight = Scroll.Height;// height of the scroll view
        double contentHeight = PokemonListLayout.Height;// total height of the content  


        if (!isLoading && scrollY + scrollViewHeight + 500 >= contentHeight)
        {
            isLoading = true;// set the loading flag to prevent multiple simultaneous loads
            await LoadPokemonRange(nextIdToLoad, nextIdToLoad + batchSize - 1,_cts.Token);// load the next batch of pokemon
            nextIdToLoad += batchSize;// update the next id to load
            isLoading = false;      // reset the loading flag
        }
    }

    private async void OnSearch(object sender, EventArgs e)
    {
        //this is an event handler for the search button 

        _cts.Cancel();// cancel any ongoing loading tasks when a new search is initiated
        //
        PokemonListLayout.Children.Clear();// clear the current list of pokemon
        string searchText = PokemonSearchBar.Text?.Trim().ToLower();// get the search text from the search bar and trim whitespace and convert to lowercase for case-insensitive search
        if (string.IsNullOrEmpty(searchText)) {

            

            LoadInitialPokemon();// if the search text is empty, reload the initial batch of pokemon



            return; }
           

        PokemonListLayout.Children.Clear();// clear the current list of pokemon

        try
        {

            //here we try to add the searched pokemon by calling the createpoke method with the search text which can be either a name or id
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

            //if the pokemon is not found or an error occurs, show an alert
            await DisplayAlert("Not Found", $"No Pokémon found with name or id '{searchText}'", "OK");
        }
    }


    async void ShowPokemonPopup(Pokemon pokemon)// this method pulls up a popup with more details about the pokemon when its tapped also saves the pokemon as veiwed in local storage
    {
        await Navigation.PushModalAsync(new PokemonPopupPage(pokemon));// method to show a popup with more details about the pokemon when its tapped

        stored.SavePokemonVeiwed(pokemon);// saving the pokemon as viewed when its tapped using the method from the StorePokemon class check that class for more details
    }


    private async void OnTypeSelected(object sender,EventArgs e)
    {


        //here we get the selected type then we go through each pokmon id if that pokemon has the specified type the only we add tat pokemon to the UI

        _cts.Cancel();

        
        if (TypePicker.SelectedIndex == -1)
            return;

        string selectedType = TypePicker.SelectedItem.ToString();// get the selected type from the picker

        PokemonListLayout.Children.Clear();

        for (int id = 1; id < 1010; id++)
        {

            
            var pokemon = await CreatePoke(id);

            if (pokemon.Types.Any(t => t.Type.Name == selectedType))
            {
                await AddPokemon(pokemon);// add the pokemon to the UI if it has the selected type
            }
        }
    }


}
