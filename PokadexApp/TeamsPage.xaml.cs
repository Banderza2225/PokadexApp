 

namespace PokadexApp;

public partial class TeamsPage : ContentPage
{

    
    TeamSelection Ts;
    public TeamsPage()
    {
        InitializeComponent();
        TeamManager.LoadTeams();
        LoadTeams();
       
        Theme.ApplyTheme(Preferences.Get("Dark", false));
    }


    public async void Create(object sender, EventArgs e)
    {
        string teamName = await DisplayPromptAsync("Create Team", "Enter a team name:");

        while (teamName == "")
        {
            teamName = await DisplayPromptAsync("Create Team", "Enter a team name:");

        }
       
            PokemonTeam Team = new PokemonTeam(teamName);

            TeamManager.AddTeam(Team);
            PokemonListLayout.Children.Clear();
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
            HorizontalStackLayout layout1 = new HorizontalStackLayout();

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

                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    img.WidthRequest = 50;
                    img.HeightRequest = 50;
                }
                layout.Children.Add(img);
            }
            

            


            frame.Content = layout;
            var tap = new TapGestureRecognizer();
            var tap1 = new TapGestureRecognizer { NumberOfTapsRequired=3};


            tap.Tapped += (s, e) => DisplayTeam(team);
            tap1.Tapped += (s, e) => DeleteTeam(team);



            frame.GestureRecognizers.Add(tap);

            

            MainThread.BeginInvokeOnMainThread(() =>
            {
                PokemonListLayout.Children.Add(frame);
            });
        }

       

    }

    public async void DeleteTeam(PokemonTeam team)
    {

       
        bool confirm = await DisplayAlert("Delete Team", $"Are you sure you want to delete the team '{team.Name}'?", "Yes", "No");
        if (confirm)
        {
            TeamManager.RemoveTeam(team);
            PokemonListLayout.Children.Clear();
            LoadTeams();
        }
    }
    public async void Reload(object sender,EventArgs e) { 
        PokemonListLayout.Children.Clear();
        LoadTeams();
    }
   

    public async void DisplayTeam(PokemonTeam team)
    {
        await Navigation.PushModalAsync(new TeamsStatsPage(team));

        
    }
}