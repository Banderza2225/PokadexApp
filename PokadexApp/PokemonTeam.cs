using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokadexApp
{
    public class PokemonTeam
    {
        //A class to represent a team of Pokemon
        public string Name { get; set; }//The name of the team
        public List<Pokemon> Team { get; set; } = new List<Pokemon>();//The list of Pokemon in the team

        public PokemonTeam(string name)//Constructor to initialize the team with a name
        {
            Name = name;
        }

        public void AddPokemon(Pokemon pokemon) {

            if (Team.Count<6)
            { //we check if the team has less than 6 pokemon before adding a new one since a team can only have 6 pokemon
                Team.Add(pokemon);//we add the pokemon to the team

            }
        
        
        }

        public void RemovePokemon(Pokemon pokemon)//method to remove a pokemon from the team
        {
            Team.Remove(pokemon);//we remove the pokemon from the team


        }

    }

}
