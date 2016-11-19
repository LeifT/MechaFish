using MechaFish.Wow.Utils;

namespace MechaFish.Wow {
    public class WowBackpack : IContainer {
        public WowBackpack(uint inventorySlotAddress) {
            ItemSlots = inventorySlotAddress + 23*WowGuid.Size;
        }

        public uint NumberOfSlots => 16;
        public uint ItemSlots { get; }
    }
}