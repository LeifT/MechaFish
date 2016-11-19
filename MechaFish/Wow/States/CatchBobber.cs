using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MechaFish.FSM;
using MechaFish.Wow.ObjectManager;

namespace MechaFish.Wow.States {
    public class CatchBobber : State {
        public override int Priority => 2;

        private WowGameObject _bobber;
        private readonly Random _rand = new Random();

        public override bool NeedToRun {

            get {
                _bobber = ObjectManager.ObjectManager.GetObjectsOfType<WowGameObject>().Where((gameObject => gameObject.CreatedBy == ObjectManager.ObjectManager.LocalPlayer.Guid && gameObject.IsBobbing)).FirstOrDefault();
                return _bobber != null && _bobber.IsValid;
            }
        }

        public override void Run() {
            Thread.Sleep(_rand.Next(240, 340));
            if (_bobber.IsMouseOver || _bobber.SetMouseOver()) {
                Keyboard.KeyPress(Keys.I);
                //Keyboard.KeyPress(Properties.KeyBindings.Default.Interact);
                Thread.Sleep(500);
            }
        }

        public override void Enter() {
            //ServiceLocator.Current.GetInstance<StatisticsViewModel>().FishLooted++;
            //ServiceLocator.Current.GetInstance<StatisticsViewModel>().Found();
        }
    }
}