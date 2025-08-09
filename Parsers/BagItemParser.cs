using PokemonSaveFileEditor.Enums;
using PokemonSaveFileEditor.Models;

namespace PokemonSaveFileEditor.Parsers
{
    public static class BagItemParser
    {
        private const int offset = 0x25C9;

        public static ItemSlot[] ReadFromByteArray(byte[] save)
        {
            int itemCount = save[offset]; // Number of items in the bag
            ItemSlot[] items = new ItemSlot[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                int baseOffset = offset + 1 + i * 2;
                byte itemId = save[baseOffset];

                // 0xFF is the terminator, but itemCount should already prevent over-reading
                if (itemId == 0xFF)
                    break;

                byte quantity = save[baseOffset + 1];

                items[i] = new ItemSlot
                {
                    Item = (Item)itemId,
                    Quantity = quantity
                };
            }
            return items;
        }

        public static byte[] WriteToByteArray(byte[] save, ItemSlot[] items)
        {
            if (save == null)
                throw new ArgumentNullException(nameof(save));
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            // Write the item count at the offset
            save[offset] = (byte)items.Length;

            for (int i = 0; i < items.Length; i++)
            {
                int baseOffset = offset + 1 + i * 2;

                if (baseOffset + 1 >= save.Length)
                    throw new IndexOutOfRangeException($"Save file too small to write item at index {i}.");

                if (items[i] == null)
                    throw new ArgumentNullException($"items[{i}] is null.");

                save[baseOffset] = (byte)items[i].Item;
                save[baseOffset + 1] = (byte)items[i].Quantity;
            }

            // Write terminator
            int terminatorOffset = offset + 1 + items.Length * 2;
            if (terminatorOffset + 1 < save.Length)
            {
                save[terminatorOffset] = 0xFF;
                save[terminatorOffset + 1] = 0x00;
            }
            return save;
        }
    }
}