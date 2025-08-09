using PokemonSaveFileEditor.Enums;

namespace PokemonSaveFileEditor.Models
{
    public class Party
    {
        public int PartyCount { get; set; }
        public Species[] SpeciesIndexes { get; set; } 
        public Pokemon[] PartyPokemon { get; set; } 
        public string[] OriginalTrainerNames { get; set; } 
        public string[] NickNames { get; set; } 
    }
}