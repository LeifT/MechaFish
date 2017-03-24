using MechaFish.Wow.ObjectManager;

namespace MechaFish.Wow.Controlls {
    public static class MouseController {
        private static IMouseStrategy _mouseStrategy;

        public static void SetMouseStrategy(IMouseStrategy mouseStrategy) {
             _mouseStrategy = mouseStrategy;
        }
        
        public static bool SetMouseOver(WowObject wowObject) {
            return _mouseStrategy.SetMouseOver(wowObject);
        }
    }
}