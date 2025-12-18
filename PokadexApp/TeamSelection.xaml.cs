namespace PokadexApp;

public partial class TeamSelection : ContentPage
{
    Pokemon pokemon;//the pokemon to add to a team
    TeamManager TeamManager = new TeamManager();
    public TeamSelection(Pokemon pokemon)
	{
        this.pokemon = pokemon;//set the pokemon to add to a team

        InitializeComponent();
        
            LoadTeams();//load existing teams to display
    }


    public void LoadTeams()//load existing teams and display them in the UI
    {

        foreach (PokemonTeam team in TeamManager.Teams)//iterate through each team check the team manager class for more details
        {
            Frame frame = new Frame//create a frame for each team
            {
                CornerRadius = 20,
                Margin = 10,
                Padding = 10,
                BackgroundColor = Color.FromArgb("#555555")
            };

            HorizontalStackLayout layout = new HorizontalStackLayout();//create a horizontal stack layout to hold the team name and pokemon images

            Label label = new Label//create a label for the team name
            {
                Text = team.Name,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                TextColor = Colors.White

            };
            layout.Children.Add(label);//add the label to the layout

            foreach (Pokemon pokemon in team.Team)//iterate through each pokemon in the team
            {

                Image img = new Image//create an image for each pokemon
                {
                    Source = pokemon.Sprites.FrontDefault,
                    WidthRequest = 120,
                    HeightRequest = 120

                };


                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    img.WidthRequest = 50;
                    img.HeightRequest = 50;
                }
                layout.Children.Add(img);//add the image to the layout
            }
            frame.Content = layout;
            var tap = new TapGestureRecognizer();
            

                tap.Tapped += (s, e) => AddPokemonToTeam(team);//   add a tap gesture recognizer to the frame to add the pokemon to the team when tapped
            frame.GestureRecognizers.Add(tap);//add the tap gesture recognizer to the frame

            MainThread.BeginInvokeOnMainThread(() =>
            {
                PokemonListLayout.Children.Add(frame);//add the frame to the main layout on the main thread
            });
        }



    }


    public async void AddPokemonToTeam(PokemonTeam team)
    {
        team.Team.Add(pokemon);//add the pokemon to the selected team
        TeamManager.SaveTeams();//save the teams
        TeamManager.LoadTeams();//  load the teams to ensure they are updated

    }

    public async void ClosePage(object sender, EventArgs e)//closes the team selection page
    {
        await Navigation.PopModalAsync();//close the page
    }   
}