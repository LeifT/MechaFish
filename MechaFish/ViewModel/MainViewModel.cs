using GalaSoft.MvvmLight;
using MechaFish.Wow;

namespace MechaFish.ViewModel {

    public class MainViewModel : ViewModelBase {
       
        private bool _isRunning;
        private int _selectedProcess;

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
                RaisePropertyChanged();
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

            GameManager.Stop();
            _isRunning = false;
        }

        private void Start()  {
            if (_isRunning || _selectedProcess == 0) {
                return;
            }

            _isRunning = true;
            GameManager.Initialize(_selectedProcess);
            GameManager.Start();
        }

    }
}