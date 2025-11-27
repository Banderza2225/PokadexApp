using System.Text.Json.Serialization;

namespace PokadexApp
{
   public class Pokemon
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("base_experience")]
        public int BaseExperience { get; set; }

        public double Height { get; set; }
        public double Weight { get; set; }

        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonTypeWrapper> Types { get; set; } = new();

        [JsonPropertyName("abilities")]
        public List<PokemonAbilityWrapper> Abilities { get; set; } = new();

        [JsonPropertyName("stats")]
        public List<PokemonStatWrapper> Stats { get; set; } = new();

        [JsonPropertyName("sprites")]
        public PokemonSprites Sprites { get; set; }

        [JsonPropertyName("moves")]
        public List<PokemonMoveWrapper> Moves { get; set; } = new();

        [JsonPropertyName("species")]
        public PokemonSpecies Species { get; set; }
    }

    public class PokemonTypeWrapper
    {
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public PokemonType Type { get; set; }
    }

    public class PokemonType
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
        [JsonPropertyName("base_stat")]
        public int BaseStat { get; set; }

        public int Effort { get; set; }

        [JsonPropertyName("stat")]
        public PokemonStat Stat { get; set; }
    }

    public class PokemonStat
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class PokemonSprites
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }

        [JsonPropertyName("back_default")]
        public string BackDefault { get; set; }

        [JsonPropertyName("front_shiny")]
        public string FrontShiny { get; set; }

        [JsonPropertyName("back_shiny")]
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

