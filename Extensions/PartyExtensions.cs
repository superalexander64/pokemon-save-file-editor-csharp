using System.Text.Json;
using System.Text.Json.Serialization;
using PokemonSaveFileEditor.Enums;
using PokemonSaveFileEditor.Models;

namespace PokemonSaveFileEditor.Extensions
{
    public static class PartyExtensions
    {
        public static Party AddMew(this Party party)
        {
            if (party.PartyCount >= 6)
            {
                throw new InvalidOperationException("Party is full");
            }
            int partyPosition = party.PartyCount;
            party.PartyCount++;
            party.SpeciesIndexes[partyPosition] = Species.MEW;
            party.NickNames[partyPosition] = "MEW";
            party.PartyPokemon[partyPosition] = new()
            {
                OriginalTrainerId = 12345,
                Species = Species.MEW,
                Type1 = PokemonType.PSYCHIC,
                Type2 = PokemonType.PSYCHIC,
                Level = 7,
                Experience = 125,
                CurrentHp = 20,
                StatusCondition = StatusCondition.OK,

                // Mew obtained through the glitch usually only knows Pound at level 7
                Move1 = Move.POUND,
                Move2 = Move.NONE,
                Move3 = Move.NONE,
                Move4 = Move.NONE,

                Move1Pp = 35,
                Move2Pp = 0,
                Move3Pp = 0,
                Move4Pp = 0,

                MaxHp = 20,
                Attack = 12,
                Defense = 11,
                Speed = 14,
                Special = 13,

                IvData = 0xAAAA, // Just a sample IV pattern
                HpIv = 10,
                AttackIv = 10,
                DefenseIv = 10,
                SpeedIv = 10,
                SpecialIv = 10,

                HpEv = 0,
                AttackEv = 0,
                DefenseEv = 0,
                SpeedEv = 0,
                SpecialEv = 0
            };        
            return party;
        }

        public static Party EvolvePokemon(this Party party, int partyPosition, Species evolveTo)
        {
            Pokemon pokemon = party.PartyPokemon[partyPosition];
            var level = pokemon.Level;

            int IV(int v) => v;
            int EV(int v) => (int)Math.Floor(Math.Sqrt(v));

            void RecalculateStats(int baseHp, int baseAtk, int baseDef, int baseSpd, int baseSpc)
            {
                pokemon.MaxHp = (((baseHp + IV(pokemon.HpIv)) * 2 + EV(pokemon.HpEv)) * level / 100) + level + 10;
                pokemon.Attack = (((baseAtk + IV(pokemon.AttackIv)) * 2 + EV(pokemon.AttackEv)) * level / 100) + 5;
                pokemon.Defense = (((baseDef + IV(pokemon.DefenseIv)) * 2 + EV(pokemon.DefenseEv)) * level / 100) + 5;
                pokemon.Speed = (((baseSpd + IV(pokemon.SpeedIv)) * 2 + EV(pokemon.SpeedEv)) * level / 100) + 5;
                pokemon.Special = (((baseSpc + IV(pokemon.SpecialIv)) * 2 + EV(pokemon.SpecialEv)) * level / 100) + 5;
                pokemon.CurrentHp = pokemon.MaxHp;
            }

            switch (evolveTo)
            {
                case Species.ALAKAZAM when pokemon.Species == Species.KADABRA:
                    party.SpeciesIndexes[partyPosition] = Species.ALAKAZAM;
                    party.NickNames[partyPosition] = "ALAKAZAM";
                    pokemon.Species = Species.ALAKAZAM;
                    RecalculateStats(baseHp: 55, baseAtk: 50, baseDef: 45, baseSpd: 120, baseSpc: 135);
                    break;

                case Species.GENGAR when pokemon.Species == Species.HAUNTER:
                    party.SpeciesIndexes[partyPosition] = Species.GENGAR;
                    party.NickNames[partyPosition] = "GENGAR";
                    pokemon.Species = Species.GENGAR;
                    RecalculateStats(baseHp: 60, baseAtk: 65, baseDef: 60, baseSpd: 110, baseSpc: 130);
                    break;

                case Species.MACHAMP when pokemon.Species == Species.MACHOKE:
                    party.SpeciesIndexes[partyPosition] = Species.MACHAMP;
                    party.NickNames[partyPosition] = "MACHAMP";
                    pokemon.Species = Species.MACHAMP;
                    RecalculateStats(baseHp: 90, baseAtk: 130, baseDef: 80, baseSpd: 55, baseSpc: 65);
                    break;

                case Species.GOLEM when pokemon.Species == Species.GRAVELER:
                    party.SpeciesIndexes[partyPosition] = Species.GOLEM;
                    party.NickNames[partyPosition] = "GOLEM";
                    pokemon.Species = Species.GOLEM;
                    RecalculateStats(baseHp: 80, baseAtk: 110, baseDef: 130, baseSpd: 45, baseSpc: 55);
                    break;

                case Species.RAICHU when pokemon.Species == Species.PIKACHU:
                    party.SpeciesIndexes[partyPosition] = Species.RAICHU;
                    party.NickNames[partyPosition] = "RAICHU";
                    pokemon.Species = Species.RAICHU;
                    RecalculateStats(baseHp: 60, baseAtk: 90, baseDef: 55, baseSpd: 100, baseSpc: 90);
                    break;
            }
            party.PartyPokemon[partyPosition] = pokemon;
            return party;
        }
        
        public static void SaveToJsonFile(this Party party, string jsonFilePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() } // serialize enums as names, not numbers
            };

            string json = JsonSerializer.Serialize(party, options);
            File.WriteAllText(jsonFilePath, json);
        }

        public static void SaveMovesToJsonFile(this Party party, string jsonFilePath)
        {
            var moveData = new Dictionary<string, List<string>>();

            for (int i = 0; i < party.PartyCount; i++)
            {
                var p = party.PartyPokemon[i];
                var species = p.Species;
                var moves = new List<string>
                {
                    p.Move1.ToString().ToUpper(),
                    p.Move2.ToString().ToUpper(),
                    p.Move3.ToString().ToUpper(),
                    p.Move4.ToString().ToUpper()
                };

                moveData[species.ToString()] = moves;
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(moveData, options);
            File.WriteAllText(jsonFilePath, json);
        }

    }
}