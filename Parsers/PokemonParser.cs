using PokemonSaveFileEditor.Enums;
using PokemonSaveFileEditor.Models;

namespace PokemonSaveFileEditor.Parsers
{
    public static class PokemonParser 
    {
        private const int partyDataOffset = 0x2F2C;
        private const int fullPartyOffset = partyDataOffset + 0x08;
        private const int entrySize = 0x2C;   // 44 bytes per Pokémon
        private const int maxPartySize = 6;

        public static Pokemon[] ReadFromByteArray(byte[] data)
        {
            int numPartyPokemon = data[partyDataOffset];
            var party = new Pokemon[maxPartySize];

            int Read16(int offset) => (data[offset] << 8) | data[offset + 1];
            int Read24(int offset) => (data[offset] << 16) | (data[offset + 1] << 8) | data[offset + 2];

            for (int i = 0; i < numPartyPokemon; i++)
            {
                int baseOffset = fullPartyOffset + i * entrySize;
                var pokemon = new Pokemon
                {
                    Species = (Species)data[baseOffset + 0x00],
                    CurrentHp = Read16(baseOffset + 0x01),
                    StatusCondition = (StatusCondition)data[baseOffset + 0x04],
                    Type1 = (PokemonType)data[baseOffset + 0x05],
                    Type2 = (PokemonType)data[baseOffset + 0x06],

                    Move1 = (Move)data[baseOffset + 0x08],
                    Move2 = (Move)data[baseOffset + 0x09],
                    Move3 = (Move)data[baseOffset + 0x0A],
                    Move4 = (Move)data[baseOffset + 0x0B],

                    OriginalTrainerId = Read16(baseOffset + 0x0C),
                    Experience = Read24(baseOffset + 0x0E),

                    HpEv = Read16(baseOffset + 0x11),
                    AttackEv = Read16(baseOffset + 0x13),
                    DefenseEv = Read16(baseOffset + 0x15),
                    SpeedEv = Read16(baseOffset + 0x17),
                    SpecialEv = Read16(baseOffset + 0x19),

                    IvData = Read16(baseOffset + 0x1B),
                    Move1Pp = data[baseOffset + 0x1D],
                    Move2Pp = data[baseOffset + 0x1E],
                    Move3Pp = data[baseOffset + 0x1F],
                    Move4Pp = data[baseOffset + 0x20],

                    Level = data[baseOffset + 0x21],
                    MaxHp = Read16(baseOffset + 0x22),
                    Attack = Read16(baseOffset + 0x24),
                    Defense = Read16(baseOffset + 0x26),
                    Speed = Read16(baseOffset + 0x28),
                    Special = Read16(baseOffset + 0x2A),
                };

                pokemon.AttackIv = (pokemon.IvData >> 12) & 0xF;
                pokemon.DefenseIv = (pokemon.IvData >> 8) & 0xF;
                pokemon.SpeedIv = (pokemon.IvData >> 4) & 0xF;
                pokemon.SpecialIv = pokemon.IvData & 0xF;
                pokemon.HpIv = ((pokemon.AttackIv & 1) << 3) |
                            ((pokemon.DefenseIv & 1) << 2) |
                            ((pokemon.SpeedIv & 1) << 1) |
                            (pokemon.SpecialIv & 1);

                party[i] = pokemon;
            }

            return party;
        }

        public static byte[] WriteToByteArray(byte[] byteArray, Pokemon[] party)
        {
            void Write16(int offset, int value)
            {
                byteArray[offset] = (byte)((value >> 8) & 0xFF);
                byteArray[offset + 1] = (byte)(value & 0xFF);
            }

            void Write24(int offset, int value)
            {
                byteArray[offset] = (byte)((value >> 16) & 0xFF);
                byteArray[offset + 1] = (byte)((value >> 8) & 0xFF);
                byteArray[offset + 2] = (byte)(value & 0xFF);
            }

            // Write party count
            byteArray[partyDataOffset] = (byte)party.Length;

            for (int i = 0; i < party.Length; i++)
            {
                var pokemon = party[i];
                int baseOffset = fullPartyOffset + i * entrySize;

                byteArray[baseOffset + 0x00] = (byte)pokemon.Species;
                Write16(baseOffset + 0x01, pokemon.CurrentHp);
                byteArray[baseOffset + 0x04] = (byte)pokemon.StatusCondition;
                byteArray[baseOffset + 0x05] = (byte)pokemon.Type1;
                byteArray[baseOffset + 0x06] = (byte)pokemon.Type2;

                byteArray[baseOffset + 0x08] = (byte)pokemon.Move1;
                byteArray[baseOffset + 0x09] = (byte)pokemon.Move2;
                byteArray[baseOffset + 0x0A] = (byte)pokemon.Move3;
                byteArray[baseOffset + 0x0B] = (byte)pokemon.Move4;

                Write16(baseOffset + 0x0C, pokemon.OriginalTrainerId);
                Write24(baseOffset + 0x0E, pokemon.Experience);

                Write16(baseOffset + 0x11, pokemon.HpEv);
                Write16(baseOffset + 0x13, pokemon.AttackEv);
                Write16(baseOffset + 0x15, pokemon.DefenseEv);
                Write16(baseOffset + 0x17, pokemon.SpeedEv);
                Write16(baseOffset + 0x19, pokemon.SpecialEv);

                Write16(baseOffset + 0x1B, pokemon.IvData);
                byteArray[baseOffset + 0x1D] = (byte)pokemon.Move1Pp;
                byteArray[baseOffset + 0x1E] = (byte)pokemon.Move2Pp;
                byteArray[baseOffset + 0x1F] = (byte)pokemon.Move3Pp;
                byteArray[baseOffset + 0x20] = (byte)pokemon.Move4Pp;

                byteArray[baseOffset + 0x21] = (byte)pokemon.Level;
                Write16(baseOffset + 0x22, pokemon.MaxHp);
                Write16(baseOffset + 0x24, pokemon.Attack);
                Write16(baseOffset + 0x26, pokemon.Defense);
                Write16(baseOffset + 0x28, pokemon.Speed);
                Write16(baseOffset + 0x2A, pokemon.Special);
            }
            return byteArray;
        }
    }
}