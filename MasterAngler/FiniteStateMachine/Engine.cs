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
            // We want to round a bit here.
            var sleepTime = 1000/framesPerSecond;

            // Leave it as a background thread. This CAN trail off
            // as the program exits, without any issues really.
            _workerThread = new Thread(Run) {IsBackground = true};
            _workerThread.Start(sleepTime);
        }

        private void Run(object sleepTime) {
            try {
                // This will immitate a games FPS
                // and attempt to 'pulse' each frame

                Running = true;

                while (Running) {
                    Pulse();
                    // Sleep for a 'frame'
                    Thread.Sleep((int) sleepTime);
                }
            } finally {
                // If we exit due to some exception,
                // that isn't caught elsewhere,
                // we need to make sure we set the Running
                // property to false, to avoid confusion,
                // and other bugs.
                Running = false;
            }
        }

        public void StopEngine() {
            if (!Running) {
                // Nothing to do.
                return;
            }
            if (_workerThread.IsAlive) {
                _workerThread.Abort();
            }
            // Clear out the thread object.
            _workerThread = null;
            // Make sure we let everyone know, we're not running anymore!
            Running = false;
        }
    }
}