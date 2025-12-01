

using Microsoft.Maui.Controls;


namespace PokadexApp
{
   

    public partial class MainPage : ContentPage
    {
      List<Pokemon> Favourites = new List<Pokemon>();
        StorePokemon stored = new StorePokemon();

        public MainPage()
        {
            InitializeComponent();
            Theme.ApplyTheme(Preferences.Get("Dark", false));
            LoadPokemon();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            
        }

        public async Task LoadPokemon()
        {
            
            Favourites = stored.LoadFavouritePokemon();

            foreach (var item in Favourites)
            {

                

                await AddPokemon(item);
            }
            
            
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
            await Navigation.PushModalAsync(new PokemonPopupPage(pokemon));
            stored.SavePokemonVeiwed(pokemon);

        }


        private async void ReloadPokemon(object sender, EventArgs e)
        {
            PokemonListLayout.Children.Clear(); 
            await LoadPokemon();  
        }

        public void CloseHistory(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        public async void DeleteHistory(object sender, EventArgs e)
        {
            stored.EraseFavourites();

            PokemonListLayout.Children.Clear();
            await LoadPokemon();
        }

    }
}
