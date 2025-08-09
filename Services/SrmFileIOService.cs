namespace PokemonSaveFileEditor.Services
{
    public static class SrmFileIOService
    {
        public static byte[] ReadByteArrayFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The file '{path}' was not found.");
            }

            return File.ReadAllBytes(path);
        }

        public static void WriteByteArrayToFile(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }

        public static byte[] UpdateChecksum(byte[] saveData)
        {
            // --- PRIMARY CHECKSUM (1-byte sum of save block, inverted) ---
            int sum = 0;
            for (int i = 0x2598; i < 0x3523; i++)
            {
                sum += saveData[i];
            }
            byte checksum = (byte)((0xFF - (sum & 0xFF)) & 0xFF);
            saveData[0x3523] = checksum;

            // --- BACKUP BLOCK COPY ---
            // Copy main save block (0x2009–0x3524) to backup region (0x3525–0x4A40)
            int length = 0x3525 - 0x2009;
            Array.Copy(saveData, 0x2009, saveData, 0x3525, length);

            // --- BACKUP CHECKSUM (same method, applied to backup block) ---
            int backupSum = 0;
            for (int i = 0x3525; i < 0x4A3F; i++)
            {
                backupSum += saveData[i];
            }
            byte backupChecksum = (byte)((0xFF - (backupSum & 0xFF)) & 0xFF);
            saveData[0x4A3F] = backupChecksum;

            return saveData;
        }
    }
}