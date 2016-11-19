using System;
using System.Text;
using MechaFish.Wow.Patch;
using MechaFish.Wow.Utils;

namespace MechaFish.Wow.ObjectManager {
    public class WowLocalPlayer : WowPlayer {
        public readonly WowBackpack Backpack;

        public WowLocalPlayer(uint address) : base(address) {
            Backpack = new WowBackpack(Descriptor + Descriptors.PlayerFields.InvSlots);
        }

        //public WowLocalPlayer() : base(0) {}

        public bool IsLooting => Memory.GameMemory.Read<byte>(Memory.BaseAddress + Addresses.Player.IsLooting) > 0;
        public bool IsTexting => Memory.GameMemory.Read<byte>(Memory.BaseAddress + Addresses.Player.IsTexting) > 0;

        public uint BagSlotsEmpty => BagSlotsMax - BagSlotsUsed;

        public uint BagSlotsMax {
            get {
                var slotsMax = Backpack.NumberOfSlots;
        
                for (uint i = 0; i < 4; i++) {
                    var bag = GetBag(i);

                    if (bag.IsValid) {
                        slotsMax += bag.NumberOfSlots;
                    } 
                }
                return slotsMax;
            }
        }

        public string Name => Memory.GameMemory.ReadCString(Memory.BaseAddress + Addresses.Player.Name, Encoding.UTF8);

        public uint BagSlotsUsed {
            get {
                var usedSlots = Backpack.UsedSlots();

                for (uint i = 0; i < 4; i++) {
                    var bag = GetBag(i);

                    if (bag.IsValid){
                        usedSlots += bag.UsedSlots();
                    }
                }
                return usedSlots;
            }
        }

        public WowGuid GetBagGuid(uint bagIndex) {
            if (bagIndex > 4) {
                throw new ArgumentOutOfRangeException();
            }

            return new WowGuid(Memory.GameMemory.ReadBytes(Memory.BaseAddress + Addresses.Player.ItemSlots + bagIndex * WowGuid.Size, WowGuid.Size));
        }

        public WowContainer GetBag(uint bagIndex) {
            return ObjectManager.GetObject<WowContainer>(GetBagGuid(bagIndex));
        }
    }
}