namespace PokadexApp;

public partial class History : ContentPage
{
    List<Pokemon> Viewed = new List<Pokemon>();// list to hold viewed pokemon
    StorePokemon storage = new StorePokemon();// calling this class to use its methods for manipulating stored viewed pokemon
    public History()
	{
		InitializeComponent();
        LoadPokemon();// loading viewed pokemon when the page is initialized
        Theme.ApplyTheme(Preferences.Get("Dark",false));// applying theme based on user preference check the Theme class for more details
    }


    public async Task LoadPokemon()
    {

        Viewed = storage.LoadViewedPokemon();// loading viewed pokemon using the method from the StorePokemon class check that class for more details

        foreach (var item in Viewed)
        {



            await AddPokemon(item);// adding each viewed pokemon to the UI using the AddPokemon method
        }


    }

    public void EraseHistory(object sender, EventArgs e)
    {
        storage.EraseHistory();//event handler to erase viewed pokemon history using the method from the StorePokemon class at the click of a button
    }

    public void CloseHistory(object sender, EventArgs e)
	{
		Navigation.PopModalAsync(); // this is for the button to close the history page and go back to the previous page
    }

   

    public async Task AddPokemon(Pokemon pokemon)/* this is our method to add viewed pokemon to
                                                   the UI this method is the eaxt same used in the MainPage for 
                                                  favourites but adapted for viewed pokemon and the same used in the pokedex page  so check the pokedex page for more details,but essentiall we take 
                                                     a pokemon object as a parameter create UI elements to display its name,id and image and add a tap gesture recognizer to show a popup with more details when tapped
                                                  
                                                  
                                                  */
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
        await Navigation.PushModalAsync(new PokemonPopupPage(pokemon));// method to show a popup with more details about the pokemon when its tapped 


    }

    public async void DeleteHistory(object sender, EventArgs e)
    {
       storage.EraseHistory();// calling the method to erase history from local storage

        PokemonListLayout.Children.Clear();//we also clear the UI elements displaying viewed pokemon essentially reloading the page
        await LoadPokemon();
    }
}