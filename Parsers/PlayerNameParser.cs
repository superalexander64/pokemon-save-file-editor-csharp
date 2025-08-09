using System.Text;

namespace PokemonSaveFileEditor.Parsers
{
    public static class PlayerNameParser 
    {
        private const int PLAYER_NAME_LENGTH = 7;
        private const int PLAYER_NAME_TOTAL_BYTES = 11;
        private const int PLAYER_NAME_OFFSET = 0x2598;
        private const byte TERMINATOR = 0x50;

        public static string ReadFromByteArray(byte[] save)
        {
            if (save == null || save.Length < PLAYER_NAME_OFFSET + PLAYER_NAME_TOTAL_BYTES)
                throw new ArgumentException("Save file is too short or null.");

            var nameBytes = new ArraySegment<byte>(save, PLAYER_NAME_OFFSET, PLAYER_NAME_TOTAL_BYTES);
            var builder = new StringBuilder();

            foreach (byte b in nameBytes)
            {
                if (CharMap.CharMapByByte.TryGetValue(b, out char value))
                {
                    builder.Append(value);
                }
                else
                {
                    builder.Append('?'); // Fallback for unknown byte
                }
            }

            string decoded = builder.ToString();
            int terminatorIndex = decoded.IndexOf('\'');

            return terminatorIndex >= 0 ? decoded.Substring(0, terminatorIndex) : decoded;
        }

        public static byte[] WriteToByteArray(byte[] saveData, string name)
        {
            name = name.ToUpperInvariant();
            if (name.Length > PLAYER_NAME_LENGTH)
                name = name.Substring(0, PLAYER_NAME_LENGTH);

            List<byte> encoded = [];
            foreach (char c in name)
            {
                if (CharMap.CharMapByChar.TryGetValue(c, out byte value))
                {
                    encoded.Add(value);
                }
                else
                {
                    encoded.Add(0xE6); // ? fallback
                }
            }
            // Pad with terminator
            while (encoded.Count < PLAYER_NAME_TOTAL_BYTES)
            {
                encoded.Add(TERMINATOR);
            }
            // Write into the save data
            Array.Copy(encoded.ToArray(), 0, saveData, PLAYER_NAME_OFFSET, PLAYER_NAME_TOTAL_BYTES);
            return saveData;
        }
    }
}