using PokemonSaveFileEditor.Models;

namespace PokemonSaveFileEditor.Parsers
{
    public static class SaveFileParser
    {
        public static SaveFile ReadFromByteArray(byte[] saveData)
        {
            return new SaveFile
            {
                PlayerName = PlayerNameParser.ReadFromByteArray(saveData),
                Party = PartyParser.ReadFromByteArray(saveData),
                BagItems = BagItemParser.ReadFromByteArray(saveData)
            };
        }

        public static byte[] WriteToByteArray(byte[] saveData, SaveFile saveFile)
        {
            saveData = PlayerNameParser.WriteToByteArray(saveData, saveFile.PlayerName);
            saveData = PartyParser.WriteToByteArray(saveData, saveFile.Party);
            saveData = BagItemParser.WriteToByteArray(saveData, saveFile.BagItems);
            return saveData;
        }
    }
}