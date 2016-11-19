using MechaFish.Wow.Patch;
using MechaFish.Wow.Utils;

namespace MechaFish.Wow.ObjectManager {
    public class WowContainer : WowItem, IContainer {
        public WowContainer(uint address) : base(address) {
            NumberOfSlots = Memory.GameMemory.Read<byte>(Descriptor + Descriptors.ContainerFields.NumSlots);
            ItemSlots = Descriptor + Descriptors.ContainerFields.Slots;
        }

        public WowContainer() : base(0) {}
        public uint NumberOfSlots { get; }
        public uint ItemSlots { get; }
    }
}