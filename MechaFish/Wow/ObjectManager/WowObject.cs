using MechaFish.Wow.Controlls;
using MechaFish.Wow.Patch;
using MechaFish.Wow.Utils;

namespace MechaFish.Wow.ObjectManager {
    public class WowObject : BaseObject {
        public WowObject(uint address) : base(address) {}

        public WowObject() : this(0) {}

        public virtual Vector3 Position {
            get {
                switch (Type) {
                    case WowObjectType.Unit: 
                    case WowObjectType.Player: {
                        return WowUnit.UnitPosition(this);
                    }

                    case WowObjectType.GameObject: {
                        return WowGameObject.ObjectPosition(this);
                    }

                    default: {
                        return Vector3.Zero;
                    }
                }
            }
        }

        public bool IsMouseOver => ObjectManager.MouseOverObject.IsValid && ObjectManager.MouseOverObject.Guid.Equals(Guid);
        public WowGuid Guid => new WowGuid(GameManager.GameMemory.ReadBytes(Descriptor, WowGuid.Size));
        public WowObjectType Type => (WowObjectType) GameManager.GameMemory.Read<int>(Pointer + Addresses.Entity.Type);
        public uint Descriptor => GameManager.GameMemory.Read<uint>(Pointer + Addresses.Entity.Descriptor);
        public int EntryId => GameManager.GameMemory.Read<int>(Descriptor + Descriptors.ObjectFields.EntryId);

        public bool SetMouseOver() {
            return MouseController.SetMouseOver(this);
        }

        public enum WowObjectType {
            Object,
            Item,
            Container,
            Unit,
            Player,
            GameObject,
            DynamicObject,
            Corpse,
            AreaTrigger,
            SceneObject
        }
    }
}