using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MasterAngler.Wow;
using MasterAngler.Wow.ObjectManager;

namespace MasterAngler.ViewModel.Data {
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class StatisticsViewModel : ViewModelBase {
        private static CancellationTokenSource _cts;
        private static CancellationToken _token;
        private double _averageFish;
        private float _emptySlots;
        private double _firstFish;
        private int _fishLooted;
        private int _gameWindowX;
        private int _gameWindowY;
        private bool _isRunning;
        private double _lastFish;
        private string _mouseOverGuid = "none";
        private string _playerName = "";
        private string _position = "unknown";
        private double _totalTime;
        private readonly List<string> all = new List<string>();
        public DateTime FoundTime;
        public DateTime StartTime;

        public StatisticsViewModel() {
            FirstFish = double.MaxValue;
            LastFish = double.MinValue;

            Messenger.Default.Register<bool>(this, UpdateMouseoverGuid);
            // Messenger.Default.Register<bool>(this, (action) => Stop());
        }

        public double FirstFish {
            get { return _firstFish; }
            set {
                _firstFish = value;
                RaisePropertyChanged();
            }
        }

        public double LastFish {
            get { return _lastFish; }
            set {
                _lastFish = value;
                RaisePropertyChanged();
            }
        }

        public double AverageFish {
            get { return _averageFish; }
            set {
                _averageFish = value;
                RaisePropertyChanged();
            }
        }

        public double TotalTime {
            get { return _totalTime; }
            set {
                _totalTime = value;
                RaisePropertyChanged();
            }
        }

        public int FishLooted {
            get { return _fishLooted; }
            set {
                _fishLooted = value;
                RaisePropertyChanged();
            }
        }

        public float EmptySlots {
            get { return _emptySlots; }
            set {
                _emptySlots = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Sets and gets the GameWindowY property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int GameWindowY {
            get { return _gameWindowY; }
            set {
                if (_gameWindowY == value) {
                    return;
                }

                _gameWindowY = value;
                RaisePropertyChanged();
            }
        }

        public int GameWindowX {
            get { return _gameWindowX; }
            set {
                if (_gameWindowX == value) {
                    return;
                }

                _gameWindowX = value;
                RaisePropertyChanged();
            }
        }

        public int GameWindowWidth {
            get { return _gameWindowWidth; }
            set {
                if (_gameWindowWidth == value) {
                    return;
                    
                }

                _gameWindowWidth = value;
                
                RaisePropertyChanged();
            }
        }

        public int MouseY {
            get { return _mouseY; }
            set {
                if (_mouseY == value) {
                    return;
                }

                _mouseY = value;
                RaisePropertyChanged();
            }
        }

        public int MouseX {
            get { return _mouseX; }
            set {
                if (_mouseX == value) {
                    return;
                }

                _mouseX = value;
                RaisePropertyChanged();
            }
        }

        public int GameWindowHeight {
            get { return _gameWindowHeight; }
            set {
                if (_gameWindowHeight == value) {
                    return;
                }

                _gameWindowHeight = value;
                RaisePropertyChanged();
            }
        }

        private int _gameWindowWidth;
        private int _gameWindowHeight;
        private int _mouseX;
        private int _mouseY;

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

        public string Position {
            get { return _position; }
            set {
                if (_position.Equals(value)) {
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

        public void Found() {
            FoundTime = DateTime.Now;

            var seconds = (FoundTime - StartTime).TotalSeconds;

            all.Add(seconds.ToString());

            TotalTime += seconds;

            if (seconds < FirstFish) {
                FirstFish = seconds;
            }

            if (seconds > LastFish) {
                LastFish = seconds;
            }

            AverageFish = TotalTime/FishLooted;
        }

        private void UpdateMouseoverGuid(bool action) {
            if (action) {
                if (_isRunning) {
                    return;
                }

                _isRunning = true;
                _cts = new CancellationTokenSource();
                _token = _cts.Token;

                Task.Run(async () => {
                    while (true) {
                        ObjectManager.Dump();

                        Point mousePos = NativeMethods.GetCursorPos();
                        MouseX = (mousePos.X / 10) - 2;
                        MouseY = (mousePos.Y / 10) - 2;

                        NativeMethods.Rect clientRect = Memory.ClientRect;
                        //NativeMethods.Rect windoWRect = NativeMethods.GetWindowRect(Memory.WindowHandle);

                        //Console.WriteLine(windoWRect.Right - windoWRect.Left);
                        //Console.WriteLine(clientRect.Right);

                        Point a = NativeMethods.ClientToScreen(Memory.WindowHandle, 0, 0);
                        

                        GameWindowX = a.X / 10;
                        GameWindowY = a.Y / 10;

                        GameWindowWidth = (clientRect.Right) / 10;
                        GameWindowHeight = (clientRect.Bottom) / 10;

                        PlayerName = ObjectManager.LocalPlayer.Name;
                        EmptySlots = ObjectManager.LocalPlayer.BagSlotsEmpty;
                        var mouse = ObjectManager.TargetObject;

                        if ((mouse != null) && mouse.IsValid) {
                            Position = mouse.Position.ToString();

                            //if (ObjectManager.LocalPlayer.IsTexting) {
                            //    mouse.SetMouseOver();
                            //}
                        }

                        await Task.Delay(0, _token).ContinueWith(tsk => { });

                        if (_cts.IsCancellationRequested) {
                            break;
                        }
                    }
                }, _token);
            } else {
                if (!_isRunning) {
                    return;
                }

                _cts.Cancel();
                _cts.Dispose();
                _isRunning = false;

                File.WriteAllLines("test.txt", all.ToArray());

                //foreach (var d in all) {
                //    file.WriteLine(d);
                //}
            }
        }
    }
}