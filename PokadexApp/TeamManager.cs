using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokadexApp
{
    internal class TeamManager
    {

        private static string FilePath => Path.Combine(FileSystem.AppDataDirectory, "teams.json");

        public static List<PokemonTeam> Teams { get; private set; } = new List<PokemonTeam>();

        public static async Task LoadTeams()
        {
            if (!File.Exists(FilePath))
            {
                Teams = new List<PokemonTeam>();
                return;
            }

            
           
                string json = await File.ReadAllTextAsync(FilePath);
                Teams = JsonSerializer.Deserialize<List<PokemonTeam>>(json) ?? new List<PokemonTeam>();
            
           
        }

        public static async Task SaveTeams()
        {

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(Teams, options);
            await File.WriteAllTextAsync(FilePath, json);



        }

        public static async void AddTeam(PokemonTeam Team) { 
        
        Teams.Add(Team);
        await SaveTeams();
        await LoadTeams();

        }

        public static async void RemoveTeam(PokemonTeam Team)
        {

            Teams.Remove(Team);
            await SaveTeams();
            await LoadTeams();  

        }

        

    }
}
