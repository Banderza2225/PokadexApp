using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokadexApp
{
    public class PokemonTeam
    {
        public string Name { get; set; }
        public List<Pokemon> Team { get; set; } = new List<Pokemon>();

        public PokemonTeam(string name)
        {
            Name = name;
        }

        public void AddPokemon(Pokemon pokemon) {

            if (Team.Count<6) { 
            Team.Add(pokemon);
            
            }
        
        
        }

        public void RemovePokemon(Pokemon pokemon)
        {
            Team.Remove(pokemon);

            
        }

    }

}
