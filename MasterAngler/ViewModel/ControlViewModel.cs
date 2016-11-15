using System;
using System.Diagnostics;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MasterAngler.FiniteStateMachine;
using MasterAngler.Wow;
using MasterAngler.Wow.ObjectManager;
using MasterAngler.Wow.States;

namespace MasterAngler.ViewModel {
    public class ControlViewModel : ViewModelBase {
        //private static readonly CancellationTokenSource _cts = new CancellationTokenSource();
        //private static CancellationToken _token;
        private bool _isRunning;
        private string _message = "Focus the Game Window";
        private int _processId;
        private bool _isStartEnabled;

        private readonly Engine _engine;

        /// <summary>
        ///     Initializes a new instance of the ControlViewModel class.
        /// </summary>
        public ControlViewModel() {
            GetGameWindow();
            _engine = new Engine();
            _engine.States.Add(new CatchBobber());
            _engine.States.Add(new Looting());
            _engine.States.Add(new CastFishing());
            // _token = _cts.Token;
        }

        public string Message {
            get { return _message; }
            set {
                if (_message.Equals(value)) {
                    return;
                }

                _message = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRunning {
            get { return _isRunning; }
            set {
                if (_isRunning == value) {
                    return;
                }

                _isRunning = value;

                if (_isRunning) {
                    ObjectManager.Start();
                    Start();
                } else {
                    ObjectManager.Stop();
                }

                RaisePropertyChanged();
            }
        }

        public bool IsStartEnabled {
            get { return _isStartEnabled; }
            set {
                if (_isRunning == value) {
                    return;
                }

                _isStartEnabled = value;
                RaisePropertyChanged();
            }
        }

        private uint GetFocusedProcessId() {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            return NativeMethods.GetWindowThreadProcessId(hwnd);
        }

        private void Start() {
            Messenger.Default.Send<bool>(true);
            _engine.StartEngine(30);
        }

        private void GetGameWindow() {
           // Task.Run(async () => {
                //while (true) {
                //    await Task.Delay(100);

                //    Process active;

                //    //try {
                //    //    active = Process.GetProcessById((int)GetFocusedProcessId());

                //    //    if (!active.MainModule.ModuleName.Equals("Wow.exe"))
                //    //    {
                //    //        continue;
                //    //    }
                //    //} catch (Exception) {
                //    //    Console.WriteLine("Hello");
                //    //    continue;
                //    //}

                    

                //    _processId = active.Id;
                //    break;
                //}
                _processId = Process.GetProcessesByName("Wow").First().Id;

                Console.WriteLine(@"Localized wow.exe");
                Memory.Initialize(_processId);
                Message = "";
                IsStartEnabled = true;
         //   });
        }
    }
}