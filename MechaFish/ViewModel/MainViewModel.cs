using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MechaFish.FSM;
using MechaFish.Wow;
using MechaFish.Wow.ObjectManager;
using MechaFish.Wow.States;
using Keyboard = MechaFish.Wow.Keyboard;

namespace MechaFish.ViewModel {

    public class MainViewModel : ViewModelBase {
        private readonly Engine _engine;

        public MainViewModel() {
            _engine = new Engine();
            _engine.States.Add(new CatchBobber());
            _engine.States.Add(new Looting());
            _engine.States.Add(new CastFishing());

            //int p = Process.GetProcessesByName("Wow").First().Id;
            //Memory.Initialize(p);

        }

        private int _selectedProcess;

        public int SelectedProcess {
            get { return _selectedProcess; }

            set {
                if (_selectedProcess == value) {
                    return;
                }

                _selectedProcess = value;
                RaisePropertyChanged();
            }
        }


        public ICommand StartCommand => new RelayCommand(Start);
        public ICommand StopCommand => new RelayCommand(Stop);
        private bool _isRunning;

        private void Stop() {

            if (!_isRunning) {
                return;
            }

            ObjectManager.Stop();
            _engine.StopEngine();

            _isRunning = false;
        }

        private void Start()  {
            if (_isRunning || _selectedProcess == 0) {
                return;
            }

            _isRunning = true;

            Memory.Initialize(_selectedProcess);

            ObjectManager.Start();
            _engine.StartEngine(30);
        }

    }
}