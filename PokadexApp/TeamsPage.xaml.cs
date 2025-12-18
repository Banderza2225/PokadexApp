 

namespace PokadexApp;

public partial class TeamsPage : ContentPage
{

    
    TeamSelection Ts;
    public TeamsPage()
    {
        InitializeComponent();
        TeamManager.LoadTeams();
        LoadTeams();// load existing teams when the page is initialized

        Theme.ApplyTheme(Preferences.Get("Dark", false));// apply theme based on user preference check the Theme class for more details
    }


    public async void Create(object sender, EventArgs e)
    {
        string teamName = await DisplayPromptAsync("Create Team", "Enter a team name:");// prompt the user to enter a team name

        while (teamName == "")// validate that the team name is not empty
        {
            teamName = await DisplayPromptAsync("Create Team", "Enter a team name:");// keep prompting until a valid name is entered

        }
        if (teamName !="" || teamName!=null)// if a valid name is entered
        {
            PokemonTeam Team = new PokemonTeam(teamName);// create a new PokemonTeam object with the entered name

            TeamManager.AddTeam(Team);//    add the new team to the TeamManager
            PokemonListLayout.Children.Clear();// clear the existing team display
            LoadTeams();// reload and display the updated list of teams
        }
        
    }
    public void LoadTeams()// load existing teams and display them in the UI
    {
       
        foreach (PokemonTeam team in TeamManager.Teams)// iterate through each team check the team manager class for more details
        {
            Frame frame = new Frame// create a frame for each team
            {
                CornerRadius = 20,
                Margin = 10,
                Padding = 10,
                BackgroundColor = Color.FromArgb("#555555")
            };

            HorizontalStackLayout layout = new HorizontalStackLayout();//create a horizontal stack layout to hold the team name and pokemon images
           

            Label label = new Label// create a label for the team name
            {
                Text = team.Name,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                TextColor = Colors.White

            };

            
            layout.Children.Add(label);// add the label to the layout

            foreach (Pokemon pokemon in team.Team)// iterate through each pokemon in the team
            {

                Image img = new Image// create an image for each pokemon
                {
                    Source = pokemon.Sprites.FrontDefault,
                    WidthRequest = 120,
                    HeightRequest = 120

                };

                if (DeviceInfo.Platform == DevicePlatform.Android)// adjust image size for Android devices
                {
                    img.WidthRequest = 50;
                    img.HeightRequest = 50;
                }
                layout.Children.Add(img);// add the image to the layout
            }
            
           

            frame.Content = layout;// set the frame content to the layout
            var tap = new TapGestureRecognizer();// create a tap gesture recognizer for single tap
           


            tap.Tapped += (s, e) => DisplayTeam(team);
          



            frame.GestureRecognizers.Add(tap);// add the tap gesture recognizer to the frame



            MainThread.BeginInvokeOnMainThread(() =>
            {
                PokemonListLayout.Children.Add(frame);// add the frame to the main layout on the UI thread
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
    public async void Reload(object sender,EventArgs e)
    {// method to reload the teams display 
        PokemonListLayout.Children.Clear();
        LoadTeams();
    }
   

    public async void DisplayTeam(PokemonTeam team)
    {
        await Navigation.PushModalAsync(new TeamsStatsPage(team));// navigate to the team stats page to view/edit the selected team


    }
}