using System.Text.Json.Serialization;

namespace PokadexApp
{
   public class Pokemon
    {

        // this class represents a Pokemon with various properties such as id, name, types, abilities, stats, sprites, moves, and species.
        public int Id { get; set; }

        [JsonPropertyName("name")]// mapping the JSON property "name" to the C# property Name
        public string Name { get; set; }

        [JsonPropertyName("base_experience")]// mapping the JSON property "base_experience" to the C# property BaseExperience
        public int BaseExperience { get; set; }

        public double Height { get; set; }
        public double Weight { get; set; }

        [JsonPropertyName("is_default")]// mapping the JSON property "is_default" to the C# property IsDefault
        public bool IsDefault { get; set; }

        [JsonPropertyName("types")]// mapping the JSON property "types" to the C# property Types
        public List<PokemonTypeWrapper> Types { get; set; } = new();

        [JsonPropertyName("abilities")]//mapping the JSON property "abilities" to the C# property Abilities
        public List<PokemonAbilityWrapper> Abilities { get; set; } = new();

        [JsonPropertyName("stats")]//mapping the JSON property "stats" to the C# property Stats
        public List<PokemonStatWrapper> Stats { get; set; } = new();

        [JsonPropertyName("sprites")]//mapping the JSON property "sprites" to the C# property Sprites
        public PokemonSprites Sprites { get; set; }

        [JsonPropertyName("moves")]//mapping the JSON property "moves" to the C# property Moves
        public List<PokemonMoveWrapper> Moves { get; set; } = new();

        [JsonPropertyName("species")]//mapping the JSON property "species" to the C# property Species
        public PokemonSpecies Species { get; set; }
    }

    public class PokemonTypeWrapper// wrapper class for Pokemon type information
    {
        public int Slot { get; set; }

        [JsonPropertyName("type")]//mapping the JSON property "type" to the C# property Type
        public PokemonType Type { get; set; }
    }

    public class PokemonType// class representing a Pokemon type
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class PokemonAbilityWrapper
    {
        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        public int Slot { get; set; }

        [JsonPropertyName("ability")]
        public PokemonAbility Ability { get; set; }
    }

    public class PokemonAbility
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class PokemonStatWrapper
    {
        [JsonPropertyName("base_stat")]//mapping the JSON property "base_stat" to the C# property BaseStat
        public int BaseStat { get; set; }

        public int Effort { get; set; }

        [JsonPropertyName("stat")]//mapping the JSON property "stat" to the C# property Stat
        public PokemonStat Stat { get; set; }
    }

    public class PokemonStat
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class PokemonSprites
    {
        [JsonPropertyName("front_default")]//mapping the JSON property "front_default" to the C# property FrontDefault
        public string FrontDefault { get; set; }

        [JsonPropertyName("back_default")]//   mapping the JSON property "back_default" to the C# property BackDefault 
        public string BackDefault { get; set; }

        [JsonPropertyName("front_shiny")]//mapping the JSON property "front_shiny" to the C# property FrontShiny
        public string FrontShiny { get; set; }

        [JsonPropertyName("back_shiny")]//mapping the JSON property "back_shiny" to the C# property BackShiny
        public string BackShiny { get; set; }

        [JsonPropertyName("other")]
        public PokemonOtherSprites Other { get; set; }
    }

    public class PokemonOtherSprites
    {
        [JsonPropertyName("official-artwork")]
        public PokemonOfficialArtwork OfficialArtwork { get; set; }
    }

    public class PokemonOfficialArtwork
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }
    }

    public class PokemonMoveWrapper
    {
        [JsonPropertyName("move")]
        public PokemonMove Move { get; set; }
    }

    public class PokemonMove
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }


    public class PokemonSpecies
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

