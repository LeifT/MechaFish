namespace MechaFish.Wow.ObjectManager {
    public class WowPlayer : WowUnit {
        public WowPlayer(uint address) : base(address) {}
        public WowPlayer() : this(0) {}
    }
}