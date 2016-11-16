using System;
using System.Threading;
using MasterAngler.FiniteStateMachine;

namespace MasterAngler.Wow.States {
    public class Looting : State {
        public override int Priority => 3;
        private int _counter;

        public override bool NeedToRun => ObjectManager.ObjectManager.LocalPlayer.IsLooting;

        public override void Run() {

            if (_counter > 20) {
                Console.WriteLine("Bags full");
            }

            Thread.Sleep(50);
            _counter++;
        }

        public override void Enter() {
            _counter = 0;
        }

        public override void Exit() {
            
        }
    }
}