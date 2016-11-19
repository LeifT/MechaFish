using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using MechaFish.Properties;

namespace MechaFish.ViewModel.Tabs {
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class HotKeysViewModel : ViewModelBase, IDataErrorInfo {
        private HotKey _castFishing;
        private HotKey _interact;
        private HotKey _pause;

        /// <summary>
        ///     Initializes a new instance of the HotKeysViewModel class.
        /// </summary>
        public HotKeysViewModel() {
            _interact = new HotKey((Key) HotKeys.Default.Interact, ModifierKeys.None);
            _castFishing = new HotKey((Key) HotKeys.Default.CastFishing, ModifierKeys.None);
            _pause = new HotKey((Key) HotKeys.Default.Pause, ModifierKeys.None);
        }

        public HotKey Interact {
            get { return _interact; }
            set {
                if (_interact == value) {
                    return;
                }

                if (value == null) {
                    HotKeys.Default.Interact = 0;
                } else {
                    HotKeys.Default.Interact = (int) value.Key;
                }

                HotKeys.Default.Save();
                _interact = value;
                RaisePropertyChanged();
            }
        }

        public HotKey CastFishing {
            get { return _castFishing; }
            set {
                if (_castFishing == value) {
                    return;
                }

                if (value == null) {
                    HotKeys.Default.CastFishing = 0;
                } else {
                    HotKeys.Default.CastFishing = (int) value.Key;
                }

                HotKeys.Default.Save();
                _castFishing = value;
                RaisePropertyChanged();
            }
        }

        public HotKey Pause {
            get { return _pause; }
            set {
                if (_pause == value) {
                    return;
                }

                if (value == null) {
                    HotKeys.Default.Pause = 0;
                } else {
                    HotKeys.Default.Pause = (int) value.Key;
                }

                HotKeys.Default.Save();
                _pause = value;
                RaisePropertyChanged();
            }
        }

        public string this[string columnName] {
            get {
                if ((columnName == nameof(Interact)) && (Interact != null) && (Interact.ModifierKeys > 0)) {
                    return $"{Interact.ModifierKeys} are not supported.";
                }

                if ((columnName == nameof(CastFishing)) && (CastFishing != null) && (CastFishing.ModifierKeys > 0)) {
                    return $"{CastFishing.ModifierKeys} are not supported.";
                }

                if ((columnName == nameof(Pause)) && (Pause != null) && (Pause.ModifierKeys > 0)) {
                    return $"{Pause.ModifierKeys} are not supported.";
                }

                return null;
            }
        }

        public string Error => string.Empty;
    }
}