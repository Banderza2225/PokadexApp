using System.Xml;
using Microsoft.Maui.Controls;

namespace PokadexApp;

public partial class TeamsPage : ContentPage
{

    TeamManager TeamManager = new TeamManager();

    public TeamsPage()
    {
        InitializeComponent();
        TeamManager.LoadTeams();

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

            Label label=new Label { 
              Text = team.Name,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                TextColor = Colors.White

            };
                layout.Children.Add(label);
            
            foreach (Pokemon pokemon in team.Team) {

                Image img = new Image
                {
                    Source=pokemon.Sprites.FrontDefault,
                    WidthRequest = 30, HeightRequest = 30

                };
               layout.Children.Add(img);
            }
            frame.Content = layout;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                PokemonListLayout.Children.Add(frame);
            });
        }


        
    }
}