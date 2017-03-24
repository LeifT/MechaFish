using MechaFish.Wow.Patch;
using MechaFish.Wow.Utils;

namespace MechaFish.Wow.ObjectManager {
    public class WowGameObject : WowObject {
        public WowGameObject(uint address) : base(address) {}
        public WowGameObject() : this(0) {}

        public override Vector3 Position => ObjectPosition(this);
        public bool IsBobbing => GameManager.GameMemory.Read<byte>(Pointer + Addresses.Object.Bobbing) > 0;
        public WowGuid CreatedBy => new WowGuid(GameManager.GameMemory.ReadBytes(Descriptor + Descriptors.GameObjectFields.CreatedBy, WowGuid.Size));
        public uint DisplayId => GameManager.GameMemory.Read<uint>(Descriptor + Descriptors.GameObjectFields.DisplayId);

        internal static Vector3 ObjectPosition(WowObject wowObject) {
            return GameManager.GameMemory.Read<Vector3>(wowObject.Pointer + Addresses.Object.Origin);
        }
    }
}