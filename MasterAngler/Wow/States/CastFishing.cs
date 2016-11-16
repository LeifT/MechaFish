using System;
using System.Threading;
using System.Windows.Forms;
using MasterAngler.FiniteStateMachine;
using MasterAngler.ViewModel.Data;
using Microsoft.Practices.ServiceLocation;

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
            Keyboard.KeyPress(Properties.KeyBindings.Default.CastFishing);
            ServiceLocator.Current.GetInstance<StatisticsViewModel>().StartTime = DateTime.Now;
            Thread.Sleep(500);
        }
    }
}