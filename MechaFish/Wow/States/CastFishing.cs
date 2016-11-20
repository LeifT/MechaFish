using System.Threading;
using System.Windows.Forms;
using MechaFish.FSM;
using MechaFish.Properties;

//using MasterAngler.ViewModel.Data;

namespace MechaFish.Wow.States {
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
            Keyboard.KeyPress(HotKeys.Default.CastFishing);
            Thread.Sleep(500);
        }
    }
}