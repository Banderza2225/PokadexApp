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

        public void RemoveFavourite(Pokemon pokemon)
        {
            string folder = FileSystem.AppDataDirectory;
            string favFile = Path.Combine(folder, $"{pokemon.Id}F.json");

            if (File.Exists(favFile))
            {
                File.Delete(favFile);
            }
        }


        public List<Pokemon> LoadFavouritePokemon()
        {
            List<Pokemon> favouritePokemon = new List<Pokemon>();
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
                    favouritePokemon.Add(pokemon);
                }
            }
            return favouritePokemon;
        }

        public List<Pokemon> LoadViewedPokemon()
        {
            List<Pokemon> viewedPokemon = new List<Pokemon>();
            string appDataDirectory = FileSystem.AppDataDirectory;
            var files = Directory.GetFiles(appDataDirectory, "*V.json");
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
                    viewedPokemon.Add(pokemon);
                }
            }
            return viewedPokemon;
        }
    }
}
