using Microsoft.Maui.Controls;

namespace PokadexApp;

public partial class TeamsStatsPage : ContentPage
{
    private PokemonTeam _team;

    public TeamsStatsPage(PokemonTeam team)
    {
        InitializeComponent();
        _team = team;

        DisplayTeam();
    }

    private void DisplayTeam()
    {
       
        TeamNameLabel.Text = _team.Name;
        TeamSizeLabel.Text = $"{_team.Team.Count}/6 Pokémon";

        PokemonListLayout.Children.Clear();

        foreach (var pokemon in _team.Team)
        {
           
            var frame = new Frame
            {
                CornerRadius = 15,
                Padding = 10,
                BackgroundColor = Color.FromArgb("#555555")
            };

            var layout = new HorizontalStackLayout { Spacing = 15 };

            
            var sprite = new Image
            {
                Source = pokemon.Sprites.FrontDefault,
                WidthRequest = 80,
                HeightRequest = 80
            };

            
            var infoLayout = new VerticalStackLayout { Spacing = 5 };

            
            infoLayout.Children.Add(new Label
            {
                Text = $"{pokemon.Name.ToUpper()} (ID: {pokemon.Id})",
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                TextColor = Colors.White
            });

           
            var types = string.Join(", ", pokemon.Types.Select(t => t.Type.Name.ToUpper()));
            infoLayout.Children.Add(new Label
            {
                Text = $"Type: {types}",
                TextColor = Colors.White,
                FontSize = 16
            });

            
            infoLayout.Children.Add(CreateStatBar("HP", pokemon.Stats.First(s => s.Stat.Name == "hp").BaseStat, 255));
            infoLayout.Children.Add(CreateStatBar("Attack", pokemon.Stats.First(s => s.Stat.Name == "attack").BaseStat, 190));
            infoLayout.Children.Add(CreateStatBar("Defense", pokemon.Stats.First(s => s.Stat.Name == "defense").BaseStat, 250));
            infoLayout.Children.Add(CreateStatBar("Sp. Atk", pokemon.Stats.First(s => s.Stat.Name == "special-attack").BaseStat, 194));
            infoLayout.Children.Add(CreateStatBar("Sp. Def", pokemon.Stats.First(s => s.Stat.Name == "special-defense").BaseStat, 250));
            infoLayout.Children.Add(CreateStatBar("Speed", pokemon.Stats.First(s => s.Stat.Name == "speed").BaseStat, 180));

            layout.Children.Add(sprite);
            layout.Children.Add(infoLayout);

            frame.Content = layout;

            PokemonListLayout.Children.Add(frame);
        }
    }

    private VerticalStackLayout CreateStatBar(string statName, int value, int maxValue)
    { 
       
        double percentage = (double)value / maxValue;

        var barLayout = new VerticalStackLayout { Spacing = 2 };

       
        barLayout.Children.Add(new Label
        {
            Text = $"{statName}: {value}",
            TextColor = Colors.White,
            FontSize = 14
        });

        
        var barBackground = new BoxView
        {
            Color = Colors.Gray,
            HeightRequest = 10,
            CornerRadius = 5,
            WidthRequest = 200
        };

        var barForeground = new BoxView
        {
            Color = Colors.LimeGreen,
            HeightRequest = 10,
            CornerRadius = 5,
            WidthRequest = 200 * percentage
        };

        
        var grid = new Grid { ColumnSpacing = 0 };
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        grid.Children.Add(barBackground);
        grid.Children.Add(barForeground);

        barLayout.Children.Add(grid);

        return barLayout;
    }

    public async void Close(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
