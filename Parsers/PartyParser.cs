using PokemonSaveFileEditor.Enums;
using PokemonSaveFileEditor.Models;

namespace PokemonSaveFileEditor.Parsers
{
    public static class PartyParser 
    {
        private const int offset = 0x2F2C;
        private const int maxPartySize = 6;
        
        public static Party ReadFromByteArray(byte[] data)
        {
            Party party = new()
            {
                PartyCount = data[offset + 0x00],
                SpeciesIndexes = new Species[maxPartySize],
                PartyPokemon = new Pokemon[maxPartySize],
                OriginalTrainerNames = new string[maxPartySize],
                NickNames = new string[maxPartySize]
            };

            // Species indexes (0x01–0x07)
            for (int i = 0; i < party.PartyCount; i++)
            {
                party.SpeciesIndexes[i] = (Species)data[offset + 0x01 + i];
            }

            party.PartyPokemon = PokemonParser.ReadFromByteArray(data);

            // OT names (start at offset + 0x110)
            for (int i = 0; i < party.PartyCount; i++)
            {
                int otOffset = offset + 0x110 + i * 0x0B;
                party.OriginalTrainerNames[i] = CharMap.DecodeString(data, otOffset, 10);
            }

            // Nicknames (start at offset + 0x152)
            for (int i = 0; i < party.PartyCount; i++)
            {
                int nickOffset = offset + 0x152 + i * 0x0B;
                party.NickNames[i] = CharMap.DecodeString(data, nickOffset, 10);
            }

            return party;
        }

        public static byte[] WriteToByteArray(byte[] byteArray, Party party)
        {
            // Write party count
            byteArray[offset + 0x00] = (byte)party.PartyCount;

            // Write species indexes
            for (int i = 0; i < party.PartyCount; i++)
            {
                byteArray[offset + 0x01 + i] = (byte)party.SpeciesIndexes[i];
            }

            // Write Pokémon data (each is 44 bytes)
            PokemonParser.WriteToByteArray(byteArray, party.PartyPokemon);
            
            // Write OT names
            for (int i = 0; i < party.PartyCount; i++)
            {
                int otOffset = offset + 0x110 + i * 0x0B;
                CharMap.EncodeString(party.OriginalTrainerNames[i] ?? "", byteArray, otOffset, 10);
            }

            // Write nicknames
            for (int i = 0; i < party.PartyCount; i++)
            {
                int nickOffset = offset + 0x152 + i * 0x0B;
                CharMap.EncodeString(party.NickNames[i] ?? "", byteArray, nickOffset, 10);
            }

            return byteArray;
        }
    }
}