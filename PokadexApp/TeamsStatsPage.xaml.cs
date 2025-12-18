

namespace PokadexApp;

public partial class TeamsStatsPage : ContentPage
{
    private PokemonTeam team;// the team whose stats are being displayed


    public TeamsStatsPage(PokemonTeam team)
    {
        InitializeComponent();
        this.team = team;// set the team

        RenderTeam();// render the team UI
        RenderStatsComparison();// render the stats comparison chart
    }

    private void RenderTeam()
    {
        
            TeamNameLabel.Text = $"{team.Name} ({team.Team.Count}/6)";// update the team name label with the current team size

        TeamGrid.Children.Clear();// clear existing team grid UI

        for (int i = 0; i < 6; i++)// iterate through 6 slots for the team
        {
            int row = i / 3;
            int col = i % 3;

            if (i < team.Team.Count)// if there is a pokemon in this slot
            {
                var pokemon = team.Team[i];
                TeamGrid.Add(CreatePokemonSlot(pokemon, i), col, row);// create and add the pokemon slot UI
            }
            else// empty slot
            {
                TeamGrid.Add(CreateEmptySlot(), col, row);// create and add the empty slot UI
            }
        }
    }

    private Frame CreatePokemonSlot(Pokemon pkm, int index)// create UI for a pokemon slot
    {
        var frame = new Frame// create a frame for the pokemon slot
        {
            CornerRadius = 15,
            Padding = 10,
            BackgroundColor = Color.FromArgb("#555555"),
            HeightRequest = 200,
        };

        var layout = new VerticalStackLayout//  create a vertical stack layout for the slot content
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        layout.Children.Add(new Image// add the pokemon image
        {
            Source = pkm.Sprites.FrontDefault,
            WidthRequest = 80,
            HeightRequest = 80
        });

        layout.Children.Add(new Label// add the pokemon name label
        {
            Text = pkm.Name.ToUpper(),
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        });

        var deleteBtn = new Button// add the remove button
        {
            Text = "Remove",
            BackgroundColor = Color.FromArgb("#FF4444"),
            TextColor = Colors.White,
            CornerRadius = 8,
            HeightRequest = 32,
            WidthRequest = 90,
            HorizontalOptions = LayoutOptions.Center
        };

        deleteBtn.Clicked += (s, e) =>  RemovePokemon(index);// attach the remove event handler

        layout.Children.Add(deleteBtn);// add the button to the layout

        frame.Content = layout;
        return frame;
    }

    private Frame CreateEmptySlot()// create UI for an empty slot
    {
        return new Frame// create a frame for the empty slot
        {
            CornerRadius = 15,
            BackgroundColor = Color.FromArgb("#333333"),
            Content = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label// add the "Empty" label
                    {
                        Text = "Empty",
                        FontSize = 16,
                        MaxLines = 1,
                        TextColor = Colors.Gray,
                        HorizontalOptions = LayoutOptions.Center
                    }
                }
            }
        };
    }

    private void RemovePokemon(int index)// remove a pokemon from the team
    {
        team.Team.RemoveAt(index);
        TeamManager.SaveTeams();
         TeamManager.LoadTeams();
        RenderTeam();
        StatsChartLayout.Children.Clear();
        RenderStatsComparison();
    }

    
    private void RenderStatsComparison()// render the stats comparison chart
    {
        if (team.Team.Count == 0)
            return;

        AddStatRow("HP");
        AddStatRow("Attack");
        AddStatRow("Defense");
        AddStatRow("Special-Attack");
        AddStatRow("Special-Defense");
        AddStatRow("Speed");
    }

    private void AddStatRow(string statName)// add a row for a specific stat
    {
        var row = new VerticalStackLayout { Spacing = 4 };// create a vertical stack layout for the stat row

        row.Children.Add(new Label// add the stat name label
        {
            Text = $" Combined {statName}",
            TextColor = Colors.DimGrey,
            FontSize = 16
        });

        var grid = new Grid// create a grid to hold the stat bars   
        {
            ColumnSpacing = 6
        };
        int statValue = 0;
        foreach (var pkm in team.Team)// calculate the total stat value for the team
        {
             statValue += GetStat(pkm, statName);
            
        }
            foreach (var pkm in     team.Team)// create a stat bar for each pokemon in the team
        {
             
            double width = statValue * 2; 

            var bar = new BoxView// create a boxview for the stat bar
            {
                Color = Colors.LimeGreen,
                HeightRequest = 14,
                WidthRequest = width,
                CornerRadius = 4
            };

            var label = new Label// create a label to display the stat value
            {
                Text = statValue.ToString(),
                TextColor = Colors.DimGrey,
                FontSize = 12
            };

            var itemLayout = new VerticalStackLayout// create a vertical stack layout for the stat bar and label
            {
                Spacing = 2,
                Children = { bar, label }
            };

            grid.Children.Add(itemLayout);// add the stat bar layout to the grid
        }

        row.Children.Add(grid);// add the grid to the stat row
        StatsChartLayout.Children.Add(row);// add the stat row to the stats chart layout
    }

    private int GetStat(Pokemon p, string statName)// get the value of a specific stat for a pokemon
    {
        return p.Stats.First(s => s.Stat.Name.Replace("-", "").Equals(// find the stat by name
            statName.Replace("-", "").ToLower()// normalize the stat name
        )).BaseStat;// return the base stat value
    }

    public async void Close(object sender, EventArgs e)// close the stats page
    {
        await Navigation.PopModalAsync();
    }

    public async void DeleteTeam(object sender, EventArgs e)// delete the entire team
    {

        TeamManager.RemoveTeam(team);// remove the team from the manager
    }
}
