using System;
using MechaFish.Wow.ObjectManager;
using MechaFish.Wow.Patch;

namespace MechaFish.Wow.Utils {
    public class BackgroundMouse : IMouseStrategy {
        public bool SetMouseOver(WowObject wowObject) {
            Memory.GameMemory.Write(new IntPtr(Memory.BaseAddress + Addresses.Player.MouseOverGuid), wowObject.Guid.ToArray());
            return wowObject.IsMouseOver;
        }
    }
}