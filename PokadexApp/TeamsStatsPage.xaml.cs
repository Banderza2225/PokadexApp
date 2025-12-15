

namespace PokadexApp;

public partial class TeamsStatsPage : ContentPage
{
    private PokemonTeam team;
    

    public TeamsStatsPage(PokemonTeam team)
    {
        InitializeComponent();
        this.team = team;

        RenderTeam();
        RenderStatsComparison();
    }

    private void RenderTeam()
    {
        
            TeamNameLabel.Text = $"{team.Name} ({team.Team.Count}/6)";

        TeamGrid.Children.Clear();

        for (int i = 0; i < 6; i++)
        {
            int row = i / 3;
            int col = i % 3;

            if (i < team.Team.Count)
            {
                var pokemon = team.Team[i];
                TeamGrid.Add(CreatePokemonSlot(pokemon, i), col, row);
            }
            else
            {
                TeamGrid.Add(CreateEmptySlot(), col, row);
            }
        }
    }

    private Frame CreatePokemonSlot(Pokemon pkm, int index)
    {
        var frame = new Frame
        {
            CornerRadius = 15,
            Padding = 10,
            BackgroundColor = Color.FromArgb("#555555"),
            HeightRequest = 200,
        };

        var layout = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        layout.Children.Add(new Image
        {
            Source = pkm.Sprites.FrontDefault,
            WidthRequest = 80,
            HeightRequest = 80
        });

        layout.Children.Add(new Label
        {
            Text = pkm.Name.ToUpper(),
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        });

        var deleteBtn = new Button
        {
            Text = "Remove",
            BackgroundColor = Color.FromArgb("#FF4444"),
            TextColor = Colors.White,
            CornerRadius = 8,
            HeightRequest = 32,
            WidthRequest = 90,
            HorizontalOptions = LayoutOptions.Center
        };

        deleteBtn.Clicked += (s, e) => RemovePokemon(index);

        layout.Children.Add(deleteBtn);

        frame.Content = layout;
        return frame;
    }

    private Frame CreateEmptySlot()
    {
        return new Frame
        {
            CornerRadius = 15,
            BackgroundColor = Color.FromArgb("#333333"),
            Content = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
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

    private void RemovePokemon(int index)
    {
        team.Team.RemoveAt(index);
        TeamManager.SaveTeams();
         TeamManager.LoadTeams();
        RenderTeam();
        StatsChartLayout.Children.Clear();
        RenderStatsComparison();
    }

    
    private void RenderStatsComparison()
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

    private void AddStatRow(string statName)
    {
        var row = new VerticalStackLayout { Spacing = 4 };

        row.Children.Add(new Label
        {
            Text = $" Combined {statName}",
            TextColor = Colors.DimGrey,
            FontSize = 16
        });

        var grid = new Grid
        {
            ColumnSpacing = 6
        };
        int statValue = 0;
        foreach (var pkm in team.Team)
        {
             statValue += GetStat(pkm, statName);
            
        }
            foreach (var pkm in     team.Team)
        {
             
            double width = statValue * 2; 

            var bar = new BoxView
            {
                Color = Colors.LimeGreen,
                HeightRequest = 14,
                WidthRequest = width,
                CornerRadius = 4
            };

            var label = new Label
            {
                Text = statValue.ToString(),
                TextColor = Colors.DimGrey,
                FontSize = 12
            };

            var itemLayout = new VerticalStackLayout
            {
                Spacing = 2,
                Children = { bar, label }
            };

            grid.Children.Add(itemLayout);
        }

        row.Children.Add(grid);
        StatsChartLayout.Children.Add(row);
    }

    private int GetStat(Pokemon p, string statName)
    {
        return p.Stats.First(s => s.Stat.Name.Replace("-", "").Equals(
            statName.Replace("-", "").ToLower()
        )).BaseStat;
    }

    public async void Close(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    public async void DeleteTeam(object sender, EventArgs e)
    {

        TeamManager.RemoveTeam(team);
    }
}
