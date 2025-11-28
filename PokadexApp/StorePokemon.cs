using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokadexApp
{
    internal class StorePokemon
    {



        public void SavePokemonVeiwed(Pokemon pokemon)
        {
            string fileName = $"{pokemon.Id}V.json";
            string path = Path.Combine(FileSystem.AppDataDirectory, fileName);

            string json = JsonSerializer.Serialize(pokemon);

            File.WriteAllText(path, json);
        }

        


        public void SavePokemonFavourite(Pokemon pokemon)
        {
            string fileName = $"{pokemon.Id}F.json";


            string path = Path.Combine(FileSystem.AppDataDirectory, fileName);




            string json = JsonSerializer.Serialize(pokemon);

            File.WriteAllText(path, json);
        }

        

        public List<Pokemon> LoadFavouritePokemons()
        {
            List<Pokemon> favouritePokemons = new List<Pokemon>();
            string appDataDirectory = FileSystem.AppDataDirectory;
            var files = Directory.GetFiles(appDataDirectory, "*F.json");
            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);
                if (pokemon != null)
                {
                    favouritePokemons.Add(pokemon);
                }
            }
            return favouritePokemons;
        }
    }
}
