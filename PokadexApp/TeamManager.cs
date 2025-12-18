using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokadexApp
{
    internal class TeamManager// this class is responsible for managing pokemon teams it provides methods to load save add and remove teams from local storage
    {

        private static string FilePath => Path.Combine(FileSystem.AppDataDirectory, "teams.json");// one set teams file where all teams are stored in json format

        public static List<PokemonTeam> Teams { get; private set; } = new List<PokemonTeam>();// list to hold all the teams

        public static async Task LoadTeams()
        {
            if (!File.Exists(FilePath))// check if the teams file exists if not initialize an empty list
            {
                Teams = new List<PokemonTeam>();// initialize empty list
                return;// exit the method
            }

            
           
                string json = await File.ReadAllTextAsync(FilePath);// read the json string from the file
            Teams = JsonSerializer.Deserialize<List<PokemonTeam>>(json) ?? new List<PokemonTeam>();// deserialize the json string back into a list of PokemonTeam objects if deserialization fails initialize an empty list


        }

        public static async Task SaveTeams()
        {

            var options = new JsonSerializerOptions// set serialization options to format the json with indentation for readability
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(Teams, options);// serialize the list of teams into a json string using the specified options
            await File.WriteAllTextAsync(FilePath, json);// write the json string to the teams file asynchronously



        }

        public static async void AddTeam(PokemonTeam Team) { 
        
        Teams.Add(Team);// add the new team to the list
            await SaveTeams();// save the updated list to storage
            await LoadTeams();// reload the teams from storage to ensure consistency

        }

        public static async void RemoveTeam(PokemonTeam Team)
        {

            Teams.Remove(Team);// remove the specified team from the list
            await SaveTeams();// save the updated list to storage
            await LoadTeams();  // reload the teams from storage to ensure consistency

        }

        

    }
}
