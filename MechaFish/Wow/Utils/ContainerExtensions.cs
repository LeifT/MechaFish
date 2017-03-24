using MechaFish.Wow.ObjectManager;

namespace MechaFish.Wow.Utils {
    public static class ContainerExtensions {
        public static uint UsedSlots(this IContainer container) {
            uint usedSlots = 0;

            var itemguides = GameManager.GameMemory.ReadBytes(container.ItemSlots, WowGuid.Size*container.NumberOfSlots);

            for (var i = 0; i < container.NumberOfSlots; i++) {
                var isEmpty = true;

                for (var j = 0; j < WowGuid.Size; j++) {
                    if (itemguides[j + i*WowGuid.Size] != 0) {
                        isEmpty = false;
                        break;
                    }
                }

                if (!isEmpty) {
                    usedSlots++;
                }
            }
            return usedSlots;
        }

        public static WowGuid GetItemGuid(this IContainer container, uint slotIndex) {
            return new WowGuid(GameManager.GameMemory.ReadBytes(container.ItemSlots + slotIndex * WowGuid.Size, WowGuid.Size));
        }

        public static WowItem GetItem(this IContainer container, uint slotIndex) {
            WowGuid guid = GetItemGuid(container, slotIndex);
            return ObjectManager.ObjectManager.GetObject<WowItem>(guid);
        }
    }
}