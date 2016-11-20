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
        private bool _isRunning;
        private int _selectedProcess;

        //public ICommand StartCommand => new RelayCommand(Toggle);

        public MainViewModel() {
            _engine = new Engine();
            _engine.States.Add(new CatchBobber());
            _engine.States.Add(new Looting());
            _engine.States.Add(new CastFishing());
        }

        public int SelectedProcess {
            get { return _selectedProcess; }

            set {
                if (_selectedProcess == value) {
                    return;
                }

                _selectedProcess = value;
                RaisePropertyChanged();

                if (_selectedProcess == 0) {
                    Stop();
                }

                RaisePropertyChanged(nameof(IsProcessSelected));
            }
        }

        public bool IsRunning {
            get {
                return _isRunning;
            }

            set {
                if (_isRunning == value) {
                    return;
                }

                Toggle();
                
            }
        }

        public bool IsProcessSelected => _selectedProcess > 0;

        private void Toggle() {
            if (_isRunning) {
                Stop();
            } else {
                Start();
            }
        }

        private void Stop() {
            if (!_isRunning) {
                return;
            }

            ObjectManager.Stop();
            _engine.StopEngine();
            _isRunning = false;
            RaisePropertyChanged(nameof(IsRunning));
        }

        private void Start()  {
            if (_isRunning || _selectedProcess == 0) {
                return;
            }

            _isRunning = true;
            Memory.Initialize(_selectedProcess);
            ObjectManager.Start();
            _engine.StartEngine(30);
            RaisePropertyChanged(nameof(IsRunning));
        }

    }
}