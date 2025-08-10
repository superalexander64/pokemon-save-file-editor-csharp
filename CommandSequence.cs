using PokemonSaveFileEditor.Enums;
using PokemonSaveFileEditor.Models;
using PokemonSaveFileEditor.Parsers;
using PokemonSaveFileEditor.Extensions;
using PokemonSaveFileEditor.Services;

namespace PokemonSaveFileEditor
{
    public static class CommandSequence
    {
        public static void Command1()
        {
            string OriginalSrmPath = "Input/SaveFile1.srm";
            string ModifiedSrmPath = "Output/SaveFile1.srm";

            // Load save data from srm file
            byte[] saveData = SrmFileIOService.ReadByteArrayFromFile(OriginalSrmPath);
            SaveFile saveFile = SaveFileParser.ReadFromByteArray(saveData);

            // Write party and items to JSON files
            saveFile.Party.SaveToJsonFile("Output/SaveFile1_Party.json");
            saveFile.Party.SaveMovesToJsonFile("Output/SaveFile1_PartyMoves.json");
            saveFile.BagItems.SaveToJsonFile("Output/SaveFile1_BagItems.json");

            // Set player name
            saveFile.PlayerName = "ALEX";

            // Add Mew to the party
            saveFile.Party.AddMew();

            // Set pokemon moves
            saveFile.Party.PartyPokemon[5].Move1 = Move.PSYCHIC;
            saveFile.Party.PartyPokemon[5].Move2 = Move.THUNDERBOLT;
            saveFile.Party.PartyPokemon[5].Move3 = Move.BLIZZARD;
            saveFile.Party.PartyPokemon[5].Move4 = Move.EARTHQUAKE;

            // Evolve pokemon
            saveFile.Party.EvolvePokemon(0, Species.RAICHU);   // PIKACHU -> RAICHU
            saveFile.Party.EvolvePokemon(1, Species.GOLEM);    // GRAVELER -> GOLEM 
            saveFile.Party.EvolvePokemon(2, Species.GENGAR);   // HAUNTER -> GENGAR 
            saveFile.Party.EvolvePokemon(3, Species.ALAKAZAM); // KADABRA -> ALAKAZAM 
            saveFile.Party.EvolvePokemon(4, Species.MACHAMP);  // MACHOKE -> MACHAMP 

            // Duplicate items
            saveFile.BagItems[0].Quantity = 99; // RARECANDY
            saveFile.BagItems[1].Quantity = 99; // MASTERBALL
            saveFile.BagItems[2].Quantity = 99; // MAXPOTION

            // Write changes back to srm file
            saveData = SaveFileParser.WriteToByteArray(saveData, saveFile);
            saveData = SrmFileIOService.UpdateChecksum(saveData);
            SrmFileIOService.WriteByteArrayToFile(ModifiedSrmPath, saveData);
        }

        public static void Command2()
        {
            string OriginalSrmPath = "Input/SaveFile2.srm";
            string ModifiedSrmPath = "Output/SaveFile2.srm";

            // Load save data from srm file
            byte[] saveData = SrmFileIOService.ReadByteArrayFromFile(OriginalSrmPath);
            SaveFile saveFile = SaveFileParser.ReadFromByteArray(saveData);

            // Write party and items to JSON files
            saveFile.Party.SaveToJsonFile("Output/SaveFile2_Party.json");
            saveFile.Party.SaveMovesToJsonFile("Output/SaveFile2_PartyMoves.json");
            saveFile.BagItems.SaveToJsonFile("Output/SaveFile2_BagItems.json");

            // BLASTOISE
            saveFile.Party.PartyPokemon[0].Move1 = Move.HYDROPUMP;
            saveFile.Party.PartyPokemon[0].Move2 = Move.SURF;
            saveFile.Party.PartyPokemon[0].Move3 = Move.ICEBEAM;
            saveFile.Party.PartyPokemon[0].Move4 = Move.EARTHQUAKE;

            // NIDOKING
            saveFile.Party.PartyPokemon[1].Move1 = Move.EARTHQUAKE;
            saveFile.Party.PartyPokemon[1].Move2 = Move.THUNDERBOLT;
            saveFile.Party.PartyPokemon[1].Move3 = Move.ICEBEAM;
            saveFile.Party.PartyPokemon[1].Move4 = Move.STRENGTH;

            // PIKACHU
            saveFile.Party.PartyPokemon[2].Move1 = Move.THUNDER;
            saveFile.Party.PartyPokemon[2].Move2 = Move.THUNDERBOLT;
            saveFile.Party.PartyPokemon[2].Move3 = Move.THUNDERWAVE;
            saveFile.Party.PartyPokemon[2].Move4 = Move.BODYSLAM;

            // KADABRA
            saveFile.Party.PartyPokemon[3].Move1 = Move.PSYCHIC;
            saveFile.Party.PartyPokemon[3].Move2 = Move.TRIATTACK;
            saveFile.Party.PartyPokemon[3].Move3 = Move.RECOVER;
            saveFile.Party.PartyPokemon[3].Move4 = Move.REFLECT;

            // VENUSAUR
            saveFile.Party.PartyPokemon[4].Move1 = Move.RAZORLEAF;
            saveFile.Party.PartyPokemon[4].Move2 = Move.MEGADRAIN;
            saveFile.Party.PartyPokemon[4].Move3 = Move.SLEEPPOWDER;
            saveFile.Party.PartyPokemon[4].Move4 = Move.CUT;

            // CHARIZARD
            saveFile.Party.PartyPokemon[5].Move1 = Move.FIREBLAST;
            saveFile.Party.PartyPokemon[5].Move2 = Move.FLAMETHROWER;
            saveFile.Party.PartyPokemon[5].Move3 = Move.FLY;
            saveFile.Party.PartyPokemon[5].Move4 = Move.EARTHQUAKE;
            
            // Evolve pokemon
            saveFile.Party.EvolvePokemon(3, Species.ALAKAZAM); // KADABRA -> ALAKAZAM 

            // Duplicate items
            saveFile.BagItems[4].Quantity = 99; // MASTERBALL
            saveFile.BagItems[5].Quantity = 99; // MAXPOTION

            // Write changes back to srm file
            saveData = SaveFileParser.WriteToByteArray(saveData, saveFile);
            saveData = SrmFileIOService.UpdateChecksum(saveData);
            SrmFileIOService.WriteByteArrayToFile(ModifiedSrmPath, saveData);
        }
    }
}