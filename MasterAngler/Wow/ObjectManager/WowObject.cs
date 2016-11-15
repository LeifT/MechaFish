using MasterAngler.Wow.Patch;
using MasterAngler.Wow.Utils;

namespace MasterAngler.Wow.ObjectManager {
    public class WowObject : BaseObject {

        //public readonly uint Address;

        private static IMouseStrategy _mouseStrategy;

        public WowObject(uint address) : base(address) {
        }

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
        public WowGuid Guid => new WowGuid(Memory.GameMemory.ReadBytes(Descriptor, WowGuid.Size));
        public WowObjectType Type => (WowObjectType) Memory.GameMemory.Read<int>(Pointer + Addresses.Entity.Type);
        public uint Descriptor => Memory.GameMemory.Read<uint>(Pointer + Addresses.Entity.Descriptor);
        public int EntryId => Memory.GameMemory.Read<int>(Descriptor + Descriptors.ObjectFields.EntryId);

        public static void SetMouseStrategy(IMouseStrategy mouseStrategy) {
            _mouseStrategy = mouseStrategy;
        }

        public bool SetMouseOver() {
            return _mouseStrategy.SetMouseOver(this);
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