using System.Collections.Generic;
using System.Threading;

namespace MasterAngler.FiniteStateMachine {
    public class Engine {
        private State _currentState;
        private Thread _workerThread;

        public Engine() {
            States = new List<State>();
        }

        public List<State> States { get; }
        public bool Running { get; private set; }

        public virtual void Pulse() {
            States.Sort();

            // This starts at the highest priority state,
            // and iterates its way to the lowest priority.
            foreach (var state in States) {
                if (state.NeedToRun) {
                    if (state != _currentState) {
                        _currentState?.Exit();
                        _currentState = state;
                        _currentState.Enter();
                    }

                    state.Run();
                    break;
                }
            }
        }

        public void StartEngine(byte framesPerSecond) {
            if (Running) {
                return;
            }

            var sleepTime = 1000/framesPerSecond;

            _workerThread = new Thread(Run) {IsBackground = true};
            _workerThread.Start(sleepTime);
        }

        private void Run(object sleepTime) {
            try {
                Running = true;

                while (Running) {
                    Pulse();
                    Thread.Sleep((int) sleepTime);
                }
            } finally {
                Running = false;
            }
        }

        public void StopEngine() {
            if (!Running) {
                return;
            }
            if (_workerThread.IsAlive) {
                _workerThread.Abort();
            }

            _workerThread = null;
            Running = false;
        }
    }
}