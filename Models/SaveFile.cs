using PokemonSaveFileEditor.Enums;

namespace PokemonSaveFileEditor.Models
{
    public class SaveFile
    {
        public string PlayerName { get; set; }
        public Party Party { get; set; }
        public ItemSlot[] BagItems { get; set; }
    }
}