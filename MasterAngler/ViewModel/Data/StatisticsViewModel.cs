using System;
using System.Drawing;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MasterAngler.Wow;
using MasterAngler.Wow.ObjectManager;
using MasterAngler.Wow.Utils;

namespace MasterAngler.ViewModel.Data
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StatisticsViewModel : ViewModelBase
    {
        private string _mouseOverGuid = "none";
        private string _position = "unknown";

        public float X {
            get { return _x; }
            set {
                _x = value;
                RaisePropertyChanged();
            }
        }

        public float Y {
            get { return _y; }
            set {
                _y = value; 
                RaisePropertyChanged();
            }
        }


        public float Wx
        {
            get { return _wx; }
            set
            {
                _wx = value;
                RaisePropertyChanged();
            }
        }


        public float Wy
        {
            get { return _wy; }
            set
            {
                _wy = value;
                RaisePropertyChanged();
            }
        }

        private float _wx;
        private float _wy;
        private float _z;
        private float _emptySlots;
        private float _y;
        private float _x;
        private string _playerName = "";

        public float Z {
            get { return _z; }
            set {
                _z = value;
                RaisePropertyChanged();
            }
        }

        public float EmptySlots
        {
            get { return _emptySlots; }
            set
            {
                _emptySlots = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the StatisticsViewModel class.
        /// </summary>
        public StatisticsViewModel()
        {
            Messenger.Default.Register<bool>(this, (action) => UpdateMouseoverGuid());
        }

        public string MouseOverGuid {
            get { return _mouseOverGuid; }
            set {

                if (_mouseOverGuid.Equals(value)) {
                    return;
                }

                _mouseOverGuid = value;
                RaisePropertyChanged();
            }
        }


        public string Position
        {
            get { return _position; }
            set
            {

                if (_position.Equals(value))
                {
                    return;
                }

                _position = value;
                RaisePropertyChanged();
            }
        }

        public string PlayerName {
            get { return _playerName; }
            set {
                if (_playerName.Equals(value)) {
                    return;
                }

                _playerName = value;
                RaisePropertyChanged();
            }
        }

        private void UpdateMouseoverGuid() {
            Task.Run(async () => {
                while (true) {
                    await Task.Delay(1);
           
                    ObjectManager.Dump();

                    PlayerName = ObjectManager.LocalPlayer.Name;
                    EmptySlots = ObjectManager.LocalPlayer.BagSlotsEmpty;
                    var mouse = ObjectManager.TargetObject;
                    

                    if (mouse != null && mouse.IsValid) {
                        Position = mouse.Position.ToString();

                        if (ObjectManager.LocalPlayer.IsTexting) {
                            mouse.SetMouseOver();
                        }

                        
                    }
                }
            });
        }
    }
}