using System;
using System.Windows.Forms;
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
        private Keys _castRodKey;
        private Keys _interactKey;

        /// <summary>
        /// Initializes a new instance of the KeyBindingsViewModel class.
        /// </summary>
        public HotkeysViewModel() {
            _castRodKey = Keys.None;
            _interactKey = Keys.None;
        }

        public Keys CastRodKey {
            get { return _castRodKey; }
            set {
                Console.WriteLine(value.ToString());
                _castRodKey = value;
                RaisePropertyChanged();
            }
        }

        public Keys InteractKey {
            get { return _interactKey; }
            set {
                Console.WriteLine(value.ToString());
                _interactKey = value;
                RaisePropertyChanged();
            }
        }
    }
}