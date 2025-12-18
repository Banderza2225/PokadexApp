

using Microsoft.Maui.Controls;


namespace PokadexApp
{
   

    public partial class MainPage : ContentPage
    {



      List<Pokemon> Favourites = new List<Pokemon>();// list to hold favourite pokemon
        StorePokemon stored = new StorePokemon();// calling this class to use its methods for manipulating stored favourite pokemon

        public MainPage()
        {
            InitializeComponent();
            Theme.ApplyTheme(Preferences.Get("Dark", false));// applying theme based on user preference check the Theme class for more details
            LoadPokemon();// loading favourite pokemon when the page is initialized
        }

        
        public async Task LoadPokemon()// method to load favourite pokemon
        {
            
            Favourites = stored.LoadFavouritePokemon();// loading favourite pokemon using the method from the StorePokemon class check that class for more details

            foreach (var item in Favourites)
            {

                

                await AddPokemon(item);// adding each favourite pokemon to the UI using the AddPokemon method
            }
            
            
        }

        

        public async Task AddPokemon(Pokemon pokemon)// method to add favourite pokemon to the UI for more details check the comment in the pokedex page where the same method is used but adapted for all  pokemon
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
                TextColor= Colors.White
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


        async void ShowPokemonPopup(Pokemon pokemon)
        {
            await Navigation.PushModalAsync(new PokemonPopupPage(pokemon));// method to show a popup with more details about the pokemon when its tapped
            stored.SavePokemonVeiwed(pokemon);// saving the pokemon as viewed when its tapped using the method from the StorePokemon class check that class for more details

        }


        private async void ReloadPokemon(object sender, EventArgs e)
        {
            PokemonListLayout.Children.Clear(); 
            await LoadPokemon();  
        }

        public void CloseHistory(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();// this is for the button to close the favourites page and go back to the previous page
        }
        public async void DeleteHistory(object sender, EventArgs e)// method to delete all favourite pokemon
        {
            stored.EraseFavourites();// using the method from the StorePokemon class to erase all favourite pokemon from local storage check that class for more details

            PokemonListLayout.Children.Clear();//   clearing the UI elements displaying favourite pokemon
            await LoadPokemon();// reloading the favourite pokemon which will now be empty
        }

    }
}
