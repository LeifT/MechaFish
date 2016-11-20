namespace MechaFish.Wow.Patch {
    public static class Addresses {
        public struct ObjectManager {
            public const uint EntitiyList = 0xD99190;
            public const uint FirstEntity = 0x0C;
            public const uint NextEntity = 0x44;
            public const uint Guid = 0x30;
        }

        public struct Camera {
            public const uint Struct = 0xF0AADC;
            public const uint Offset = 0x324C;
            public const uint Origin = 0x08;
            public const uint Matrix = 0x14;
            public const uint Fov = 0x38;
        }

        public struct Entity {
            public const uint Descriptor = 0x08;  // Address + 0x08
            public const uint Type = 0x10;  // Address + 0x10
        }

        public struct Object {
            public const uint Bobbing = 0xF8; // Address + this     
            public const uint Origin = 0x138; // Address + this        
        }

        public struct Player {
            public const uint LocalPlayer = 0xE35880;       // GameBaseAdress  + this
            public const uint IsLooting = 0xF1FB1D;         // GameBaseAdress  + this
            public const uint IsTexting = 0xD15F08;        // GameBaseAdress  + this
            public const uint MouseOverGuid = 0xEAD520;    // GameBaseAdress  + this
            public const uint TargetGuid = 0xF0AC20;      // GameBaseAdress  + this
            public const uint ItemSlots = 0xF1CC40;      // GameBaseAdress  + this
            public const uint Name = 0xF8BF70;      // GameBaseAdress  + this
        }

        public struct Unit {
            public const uint Origin = 0xAF8; // Address + this    
            public const uint CastingSpellId = 0x1048; // UnitAddress + this
            public const uint ChannelSpellId = 0x1098; // UnitAddress + this
            //public const uint ChannelSpellStartTime = ChannelSpellId + 4; // Address + this
            //public const uint ChannelSpellEndTime = ChannelSpellId + 8; // Address + this
        }

        //public struct Container {
        //    public const uint Slot = ;

        //    // Descriptors
        //    //public const uint Slots = 0x150; // CONTAINER_FIELD_NUM_SLOTS
        //    //public const uint ItemSlot = 0x390; // CONTAINER_FIELD_SLOT_1

        //}
    }
}