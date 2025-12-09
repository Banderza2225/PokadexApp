namespace PokadexApp;

public partial class TeamSelection : ContentPage
{
    Pokemon pokemon;
    TeamManager TeamManager = new TeamManager();
    public TeamSelection(Pokemon pokemon)
	{
        this.pokemon = pokemon;

        InitializeComponent();
        
            LoadTeams();
    }


    public void LoadTeams()
    {

        foreach (PokemonTeam team in TeamManager.Teams)
        {
            Frame frame = new Frame
            {
                CornerRadius = 20,
                Margin = 10,
                Padding = 10,
                BackgroundColor = Color.FromArgb("#555555")
            };

            HorizontalStackLayout layout = new HorizontalStackLayout();

            Label label = new Label
            {
                Text = team.Name,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                TextColor = Colors.White

            };
            layout.Children.Add(label);

            foreach (Pokemon pokemon in team.Team)
            {

                Image img = new Image
                {
                    Source = pokemon.Sprites.FrontDefault,
                    WidthRequest = 120,
                    HeightRequest = 120

                };
                layout.Children.Add(img);
            }
            frame.Content = layout;
            var tap = new TapGestureRecognizer();
            

                tap.Tapped += (s, e) => AddPokemonToTeam(team);
                frame.GestureRecognizers.Add(tap);
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PokemonListLayout.Children.Add(frame);
            });
        }



    }


    public async void AddPokemonToTeam(PokemonTeam team)
    {
        team.Team.Add(pokemon);
        TeamManager.SaveTeams();
        TeamManager.LoadTeams();

    }

    public async void ClosePage(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }   
}