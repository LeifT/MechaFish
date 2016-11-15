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
        //private Keys _castKey;
        //private Keys _interactKey;

        /// <summary>
        /// Initializes a new instance of the KeyBindingsViewModel class.
        /// </summary>
        public HotkeysViewModel() {
            //_castKey = Keys.None;
            //_interactKey = Keys.None;
        }

        //public Keys CastKey {
        //    get { return _castKey; }
        //    set {
        //        Console.WriteLine(value.ToString());
        //        _castKey = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //public Keys InteractKey {
        //    get { return _interactKey; }
        //    set {
        //        Console.WriteLine(value.ToString());
        //        _interactKey = value;
        //        RaisePropertyChanged();
        //    }
        //}
    }
}