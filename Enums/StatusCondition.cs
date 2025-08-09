namespace PokemonSaveFileEditor.Enums
{
    public enum StatusCondition : int
    {
        OK        = 0,  // Binary 00000000 
        ASLEEP    = 4,  // Binary 00000100 
        POISONED  = 8,  // Binary 00001000 
        BURNED    = 16, // Binary 00010000 
        FROZEN    = 32, // Binary 00100000 
        Paralyzed = 64  // Binary 01000000 
    }
}
