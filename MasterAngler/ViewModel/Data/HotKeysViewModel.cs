using GalaSoft.MvvmLight;

namespace MasterAngler.ViewModel.Data
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class HotkeysViewModel : ViewModelBase
    {
        private string _castRodKey;
        private string _interactKey;

        /// <summary>
        /// Initializes a new instance of the KeyBindingsViewModel class.
        /// </summary>
        public HotkeysViewModel() {
            _castRodKey = "";
            _interactKey = "";
        }

        public string CastRodKey {
            get { return _castRodKey; }
            set {
                _castRodKey = value;
                
                RaisePropertyChanged();
            }
        }

        public string InteractKey {
            get { return _interactKey; }
            set {
                _interactKey = value;
                RaisePropertyChanged();
            }
        }
    }
}