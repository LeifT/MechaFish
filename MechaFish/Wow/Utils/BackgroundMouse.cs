using System;
using MechaFish.Wow.ObjectManager;

namespace MechaFish.Wow.Utils {
    public class BackgroundMouse : IMouseStrategy {
        public bool SetMouseOver(WowObject wowObject) {
            Memory.GameMemory.Write(new IntPtr(Memory.BaseAddress + 0xEAD520), wowObject.Guid.ToArray());


            return wowObject.IsMouseOver;
        }
    }
}