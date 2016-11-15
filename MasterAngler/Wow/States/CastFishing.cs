using System.Windows.Forms;
using MasterAngler.FiniteStateMachine;

namespace MasterAngler.Wow.States {
    public class CastFishing : State {
        public override int Priority => 1;

        public override bool NeedToRun {
            get {
                if (ObjectManager.ObjectManager.LocalPlayer.IsCasting) {
                    return false;
                }

                return true;
            }
        }

        public override void Run() {
            Keyboard.KeyPress(Properties.Settings.Default.CastFishing);
        }
    }
}