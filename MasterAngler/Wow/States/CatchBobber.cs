using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MasterAngler.FiniteStateMachine;
using MasterAngler.Wow.ObjectManager;

namespace MasterAngler.Wow.States {
    public class CatchBobber : State {
        public override int Priority => 2;

        private WowGameObject _bobber;
        private readonly Random _rand = new Random();

        public override bool NeedToRun {

            get {
                _bobber = ObjectManager.ObjectManager.GetObjectsOfType<WowGameObject>().FirstOrDefault(b => b.IsBobbing);

                return _bobber != null && _bobber.IsValid;
            }
        }

        public override void Run() {
            // idle
        }

        public override void Enter() {
            int sleepTime = _rand.Next(240, 340);
            Thread.Sleep(sleepTime);
            _bobber.SetMouseOver();
            sleepTime = _rand.Next(240, 340);
            Thread.Sleep(sleepTime);
            Keyboard.KeyPress(Keys.I);
        }
    }
}