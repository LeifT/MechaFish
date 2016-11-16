using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MasterAngler.FiniteStateMachine;
using MasterAngler.ViewModel.Data;
using MasterAngler.Wow.ObjectManager;
using Microsoft.Practices.ServiceLocation;

namespace MasterAngler.Wow.States {
    public class CatchBobber : State {
        public override int Priority => 2;

        private WowGameObject _bobber;
        private readonly Random _rand = new Random();

        public override bool NeedToRun {

            get {
                _bobber =
                    ObjectManager.ObjectManager.GetObjectsOfType<WowGameObject>()
                        .Where((o => o.CreatedBy == ObjectManager.ObjectManager.LocalPlayer.Guid && o.IsBobbing))
                        .FirstOrDefault();

                return _bobber != null && _bobber.IsValid;
            }
        }

        public override void Run() {
            // idle
        }

        public override void Enter() {
            ServiceLocator.Current.GetInstance<StatisticsViewModel>().FishLooted++;
            ServiceLocator.Current.GetInstance<StatisticsViewModel>().Found();
            int sleepTime = _rand.Next(240, 340);
            Thread.Sleep(sleepTime);
            _bobber.SetMouseOver();
            //Console.WriteLine(DateTime.UtcNow);
            sleepTime = _rand.Next(240, 340);
            Thread.Sleep(sleepTime);
            //Console.WriteLine(DateTime.UtcNow);
            Keyboard.KeyPress(Properties.KeyBindings.Default.Interact);
        }
    }
}