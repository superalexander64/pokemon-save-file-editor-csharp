using System.Text;

namespace PokemonSaveFileEditor
{
    public static class CharMap
    {
        public static readonly Dictionary<byte, char> CharMapByByte = new Dictionary<byte, char>
        {
            { 0x80, 'A' }, { 0x81, 'B' }, { 0x82, 'C' }, { 0x83, 'D' },
            { 0x84, 'E' }, { 0x85, 'F' }, { 0x86, 'G' }, { 0x87, 'H' },
            { 0x88, 'I' }, { 0x89, 'J' }, { 0x8A, 'K' }, { 0x8B, 'L' },
            { 0x8C, 'M' }, { 0x8D, 'N' }, { 0x8E, 'O' }, { 0x8F, 'P' },
            { 0x90, 'Q' }, { 0x91, 'R' }, { 0x92, 'S' }, { 0x93, 'T' },
            { 0x94, 'U' }, { 0x95, 'V' }, { 0x96, 'W' }, { 0x97, 'X' },
            { 0x98, 'Y' }, { 0x99, 'Z' }, { 0x9A, '(' }, { 0x9B, ')' },
            { 0x9C, ':' }, { 0x9D, ';' }, { 0x9E, '[' }, { 0x9F, ']' },
            { 0xA0, 'a' }, { 0xA1, 'b' }, { 0xA2, 'c' }, { 0xA3, 'd' },
            { 0xA4, 'e' }, { 0xA5, 'f' }, { 0xA6, 'g' }, { 0xA7, 'h' },
            { 0xA8, 'i' }, { 0xA9, 'j' }, { 0xAA, 'k' }, { 0xAB, 'l' },
            { 0xAC, 'm' }, { 0xAD, 'n' }, { 0xAE, 'o' }, { 0xAF, 'p' },
            { 0xB0, 'q' }, { 0xB1, 'r' }, { 0xB2, 's' }, { 0xB3, 't' },
            { 0xB4, 'u' }, { 0xB5, 'v' }, { 0xB6, 'w' }, { 0xB7, 'x' },
            { 0xB8, 'y' }, { 0xB9, 'z' }, { 0xBA, 'é' }, { 0xE6, '?' },
            { 0xE7, '!' }, { 0xE8, '.' }, { 0xF2, '.' }, { 0xF3, '/' },
            { 0xF4, ',' }, { 0xF6, '0' }, { 0xF7, '1' }, { 0xF8, '2' },
            { 0xF9, '3' }, { 0xFA, '4' }, { 0xFB, '5' }, { 0xFC, '6' },
            { 0xFD, '7' }, { 0xFE, '8' }, { 0xFF, '9' }, { 0x50, '\'' }
        };

        public static readonly Dictionary<char, byte> CharMapByChar = new Dictionary<char, byte>
        {
            { 'A', 0x80 }, { 'B', 0x81 }, { 'C', 0x82 }, { 'D', 0x83 },
            { 'E', 0x84 }, { 'F', 0x85 }, { 'G', 0x86 }, { 'H', 0x87 },
            { 'I', 0x88 }, { 'J', 0x89 }, { 'K', 0x8A }, { 'L', 0x8B },
            { 'M', 0x8C }, { 'N', 0x8D }, { 'O', 0x8E }, { 'P', 0x8F },
            { 'Q', 0x90 }, { 'R', 0x91 }, { 'S', 0x92 }, { 'T', 0x93 },
            { 'U', 0x94 }, { 'V', 0x95 }, { 'W', 0x96 }, { 'X', 0x97 },
            { 'Y', 0x98 }, { 'Z', 0x99 }, { '(', 0x9A }, { ')', 0x9B },
            { ':', 0x9C }, { ';', 0x9D }, { '[', 0x9E }, { ']', 0x9F },
            { 'a', 0xA0 }, { 'b', 0xA1 }, { 'c', 0xA2 }, { 'd', 0xA3 },
            { 'e', 0xA4 }, { 'f', 0xA5 }, { 'g', 0xA6 }, { 'h', 0xA7 },
            { 'i', 0xA8 }, { 'j', 0xA9 }, { 'k', 0xAA }, { 'l', 0xAB },
            { 'm', 0xAC }, { 'n', 0xAD }, { 'o', 0xAE }, { 'p', 0xAF },
            { 'q', 0xB0 }, { 'r', 0xB1 }, { 's', 0xB2 }, { 't', 0xB3 },
            { 'u', 0xB4 }, { 'v', 0xB5 }, { 'w', 0xB6 }, { 'x', 0xB7 },
            { 'y', 0xB8 }, { 'z', 0xB9 }, { 'é', 0xBA }, { '?', 0xE6 },
            { '!', 0xE7 }, { '.', 0xF2 }, { '/', 0xF3 }, { ',', 0xF4 },
            { '0', 0xF6 }, { '1', 0xF7 }, { '2', 0xF8 }, { '3', 0xF9 },
            { '4', 0xFA }, { '5', 0xFB }, { '6', 0xFC }, { '7', 0xFD },
            { '8', 0xFE }, { '9', 0xFF }, { '\'', 0x50 }
        };


        /// <summary>
        /// Decodes a Gen I text string from save data starting at a specific offset.
        /// Stops at terminator (0x50) or when length is reached.
        /// </summary>
        public static string DecodeString(byte[] data, int offset, int length)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                byte b = data[offset + i];
                if (b == 0x50) break; // terminator

                if (CharMapByByte.TryGetValue(b, out var ch))
                    sb.Append(ch);
                else
                    sb.Append('?'); // unknown character
            }

            return sb.ToString();
        }

        /// <summary>
        /// Encodes a string into Gen I format at a specific byte array offset.
        /// Adds terminator (0x50) and pads with 0x50 if necessary.
        /// </summary>
        public static void EncodeString(string input, byte[] data, int offset, int maxLength)
        {
            int i = 0;
            for (; i < input.Length && i < maxLength; i++)
            {
                if (CharMapByChar.TryGetValue(input[i], out var b))
                    data[offset + i] = b;
                else
                    data[offset + i] = 0xE6; // fallback to '?' if unknown
            }

            // Add terminator and pad the rest
            if (i < maxLength)
                data[offset + i++] = 0x50; // terminator

            for (; i < maxLength + 1; i++) // include terminator space
                data[offset + i] = 0x50;
        }
    }
}