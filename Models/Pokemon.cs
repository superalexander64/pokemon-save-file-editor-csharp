using PokemonSaveFileEditor.Enums;

namespace PokemonSaveFileEditor.Models
{
    public class Pokemon
    {
        // Core data
        public Species Species { get; set; }
        public PokemonType Type1 { get; set; }
        public PokemonType Type2 { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int CurrentHp { get; set; }
        public StatusCondition StatusCondition { get; set; }
        public int OriginalTrainerId { get; set; }
    
        // Moves
        public Move Move1 { get; set; }
        public Move Move2 { get; set; }
        public Move Move3 { get; set; }
        public Move Move4 { get; set; }
        
        // Move PP values
        public int Move1Pp { get; set; }
        public int Move2Pp { get; set; }
        public int Move3Pp { get; set; }
        public int Move4Pp { get; set; }

        // Stats
        public int MaxHp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int Special { get; set; }

        // Individual values
        public int IvData { get; set; }
        public int HpIv { get; set; }
        public int AttackIv { get; set; }
        public int DefenseIv { get; set; }
        public int SpeedIv { get; set; }
        public int SpecialIv { get; set; }

        // Stat experience data
        public int HpEv { get; set; }
        public int AttackEv { get; set; }
        public int DefenseEv { get; set; }
        public int SpeedEv { get; set; }
        public int SpecialEv { get; set; }
    }
}