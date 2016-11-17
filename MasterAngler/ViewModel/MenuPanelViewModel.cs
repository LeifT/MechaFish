using System;
using GalaSoft.MvvmLight;

namespace MasterAngler.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MenuPanelViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MenuPanelViewModel class.
        /// </summary>
        public MenuPanelViewModel() {
            _selectedItem = "";
        }

        private string _selectedItem;
        private bool _isStatisticsVisible;
        private bool _isHotkeysVisible;
        private bool _isOptionsVisible;

        public string SelectedItem {
            
            get { return _selectedItem; }

            set {
                if (_selectedItem.Equals(value)) {
                    return;
                }
          
                Hide(_selectedItem);
                _selectedItem = value;
                Show(_selectedItem);

                RaisePropertyChanged();
            } 
        }

        private void Hide(string name) {
            switch (name) {
                case "Statistics":
                    IsStatisticsVisible = false;
                    break;
                case "Hotkeys":
                    IsHotkeysVisible = false;
                    break;
                case "Options":
                    IsOptionsVisible = false;
                    break;
            }
        }

        private void Show(string name) {
            switch (name)
            {
                case "Statistics":
                    IsStatisticsVisible = true;
                    break;
                case "Hotkeys":
                    IsHotkeysVisible = true;
                    break;
                case "Options":
                    IsOptionsVisible = true;
                    break;

            }
        }

        public bool IsStatisticsVisible {
            get { return _isStatisticsVisible; }
            set {
                _isStatisticsVisible = value;
                RaisePropertyChanged();
            }
        }

        public bool IsHotkeysVisible {
            get { return _isHotkeysVisible; }
            set {
                _isHotkeysVisible = value;
                RaisePropertyChanged();
            }
        }

        public bool IsOptionsVisible {
            get { return _isOptionsVisible; }
            set {
                _isOptionsVisible = value;
                RaisePropertyChanged();
            }
        }
    }
}