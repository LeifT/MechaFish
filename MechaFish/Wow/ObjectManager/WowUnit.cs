using MechaFish.Wow.Patch;
using MechaFish.Wow.Utils;

namespace MechaFish.Wow.ObjectManager {
    public class WowUnit : WowObject {
        public WowUnit(uint address) : base(address) {}
        public WowUnit() : this(0) {}

        public bool IsCasting => CastingSpellId > 0 || ChannelSpellId > 0;
        
        public int CastingSpellId => Memory.GameMemory.Read<int>(Pointer + Addresses.Unit.CastingSpellId);

        public int ChannelSpellId => Memory.GameMemory.Read<int>(Pointer + Addresses.Unit.ChannelSpellId);

        public override Vector3 Position => UnitPosition(this);

        public static Vector3 UnitPosition(WowObject wowObject) {
            return Memory.GameMemory.Read<Vector3>(wowObject.Pointer + Addresses.Unit.Origin);
        }
    }
}