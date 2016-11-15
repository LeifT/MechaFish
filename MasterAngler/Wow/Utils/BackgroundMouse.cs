using System;
using MasterAngler.Wow.ObjectManager;

namespace MasterAngler.Wow.Utils {
    public class BackgroundMouse : IMouseStrategy {
        public bool SetMouseOver(WowObject wowObject) {
            Memory.GameMemory.Write(new IntPtr(Memory.BaseAddress + 0xEAD520), wowObject.Guid.ToArray());


            return wowObject.IsMouseOver;
        }
    }
}