using GalaSoft.MvvmLight;
using MechaFish.Properties;
using MechaFish.Wow;
using MechaFish.Wow.Utils;

namespace MechaFish.ViewModel.Tabs {
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class OptionsViewModel : ViewModelBase {
        private bool _isBackgroundMode;

        /// <summary>
        ///     Initializes a new instance of the OptionsViewModel class.
        /// </summary>
        public OptionsViewModel() {
            _isBackgroundMode = Settings.Default.BackgroundMode;

            if (_isBackgroundMode) {
                MouseController.SetMouseStrategy(new BackgroundMouse());
            } else {
                MouseController.SetMouseStrategy(new ForegroundMouse());
            }
        }

        public bool IsBackgoundMode {
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