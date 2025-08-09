using System.Text.Json;
using System.Text.Json.Serialization;
using PokemonSaveFileEditor.Models;

namespace PokemonSaveFileEditor.Extensions
{
    public static class ItemSlotExtensions
    {
        public static void SaveToJsonFile(this ItemSlot[] items, string jsonFilePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() } 
            };
            string json = JsonSerializer.Serialize(items, options);
            File.WriteAllText(jsonFilePath, json);
        }
    }
}