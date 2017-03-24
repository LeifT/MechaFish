using System;
using MechaFish.Wow.ObjectManager;
using MechaFish.Wow.Patch;

namespace MechaFish.Wow.Controlls {
    public class BackgroundMouse : IMouseStrategy {
        public bool SetMouseOver(WowObject wowObject) {
            GameManager.GameMemory.Write(new IntPtr(GameManager.BaseAddress + Addresses.Player.MouseOverGuid), wowObject.Guid.ToArray());
            return wowObject.IsMouseOver;
        }
    }
}