using GalaSoft.MvvmLight;
using MechaFish.Properties;
using MechaFish.Wow.Controlls;

namespace MechaFish.ViewModel.Tabs {
    public class OptionsViewModel : ViewModelBase {
        private bool _isBackgroundMode;
        
        public OptionsViewModel() {
            _isBackgroundMode = Settings.Default.BackgroundMode;

            if (_isBackgroundMode) {
                MouseController.SetMouseStrategy(new BackgroundMouse());
            } else {
                MouseController.SetMouseStrategy(new ForegroundMouse());
            }
        }

        public bool IsBackgroundMode {
            get { return _isBackgroundMode; }
            set {
                if (_isBackgroundMode == value) {
                    return;
                }

                _isBackgroundMode = value;
                MouseModeChanged();
                RaisePropertyChanged();
            }
        }

        private void MouseModeChanged() {
            Settings.Default.BackgroundMode = _isBackgroundMode;
            Settings.Default.Save();

            if (_isBackgroundMode) {
                MouseController.SetMouseStrategy(new BackgroundMouse());
            } else {
                MouseController.SetMouseStrategy(new ForegroundMouse());
            }
        }
    }
}