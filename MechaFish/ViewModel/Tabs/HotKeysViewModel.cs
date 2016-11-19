using System.Windows.Input;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;

namespace MechaFish.ViewModel.Tabs
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class HotKeysViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the HotKeysViewModel class.
        /// </summary>
        public HotKeysViewModel() {}

        private HotKey _interact = new HotKey(Key.None, ModifierKeys.None);
        private HotKey _castFishing = new HotKey(Key.None, ModifierKeys.None);

        public HotKey Interact {
            get { return _interact; }
            set {
                if (_interact != value)
                {
                    _interact = value;
                    //if (_hotKey != null && _hotKey.Key != Key.None && _hotKey.Key != Key.F12)
                    //{
                    //    HotkeyManager.Current.AddOrReplace("demo", HotKey.Key, HotKey.ModifierKeys, (sender, e) => OnHotKey(sender, e));
                    //}
                    //else
                    //{

                    //    HotkeyManager.Current.Remove("demo");
                    //}
                    RaisePropertyChanged();
                }
            }
        }
    }
}