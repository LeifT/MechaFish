namespace MechaFish.Wow.ObjectManager {
    public class WowItem : WowObject {
        public WowItem(uint address) : base(address) {}

        public WowItem() : this(0) {}

        public int ItemId => EntryId;
    }
}