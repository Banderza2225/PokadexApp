using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokadexApp
{
    internal class StorePokemon
    {
        /*
         this class is responsible for saving and loading viewed and favourite Pokemon to and from local storage.
            It uses JSON serialization to store the Pokemon objects as files in the app's data directory.

        this class provides methods that are used in the history and favourites page in hindsite i should made  made the method static

        the stored pokmon also allow for offline veiwing of previously viewed or favourited pokemon
         
         
         
         
         */


        // Save a viewed Pokemon to local storage
        public void SavePokemonVeiwed(Pokemon pokemon)
        {
            string fileName = $"{pokemon.Id}V.json";/*here im using the pokemon id to create a unique filename for each viewed 
                                                      pokemon im using the V to indicate its a viewed pokemon so i can differentiate between the favourites and call each one indivisually */
            string path = Path.Combine(FileSystem.AppDataDirectory, fileName);//get the path to the app data directory and combine it with the filename

            string json = JsonSerializer.Serialize(pokemon);// here im serializing the pokemon object to json format

            File.WriteAllText(path, json);//here im writing the json string to a file at the specified path
        }




        public void SavePokemonFavourite(Pokemon pokemon)
        {
            string fileName = $"{pokemon.Id}F.json";// exact same thing with the save veiwed method but using F to indicate its a favourite pokemon rest of the code is the same


            string path = Path.Combine(FileSystem.AppDataDirectory, fileName);




            string json = JsonSerializer.Serialize(pokemon);

            File.WriteAllText(path, json);
        }

        public void RemoveFavourite(Pokemon pokemon)
        {
            string folder = FileSystem.AppDataDirectory;// get the app data directory path
            string favFile = Path.Combine(folder, $"{pokemon.Id}F.json");// create the full path to the favourite pokemon file using the pokemon id

            if (File.Exists(favFile))
            {
                File.Delete(favFile);// now we check if the file exists if it does we delete it im checking to ame sure we dont get an error trying to delete a file that doesnt exist
            }
        }


        public List<Pokemon> LoadFavouritePokemon()
        {

            // here im loading all the favourite pokemon by searching for all files in the app data directory that end with F.json
            /* 
            this method creates an empty list to store the favourite Pokemon objects.
            then it checks each file in the app data directory that matches the pattern "*F.json".
            if it does it is the file that ends with F.json it reads the file content and deserializes the json string back into a Pokemon object.
            then we add it to the favouritePokemon list.


             */
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


            // now this method is doing the exact same thing as the load favourite method but for viewed pokemon instead of favourite pokemon by checking for files that end with V.json
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




        public void EraseHistory()

        //erases all files that end with V.json to clear the viewed pokemon history
        {
            string appDataDirectory = FileSystem.AppDataDirectory;
            var files = Directory.GetFiles(appDataDirectory, "*V.json");
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        public void EraseFavourites()
        {

            //erases all files that end with F.json to clear the favourite pokemon list
            string appDataDirectory = FileSystem.AppDataDirectory;
            var files = Directory.GetFiles(appDataDirectory, "*F.json");
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }



    }
}
