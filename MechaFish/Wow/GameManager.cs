using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using MechaFish.FSM;
using MechaFish.Wow.States;

namespace MechaFish.Wow {
    public static class GameManager {
        public static uint BaseAddress { get; private set; }
        public static MemoryReader GameMemory { get; private set; }
        public static IntPtr WindowHandle { get; private set; }
        private static readonly Engine _engine;
        private static GameData gameData;

        static GameManager() {
            GameMemory = new MemoryReader();
            gameData = new GameData();
            

            _engine = new Engine();
            _engine.States.Add(new CatchBobber());
            _engine.States.Add(new Looting());
            _engine.States.Add(new CastFishing());
        }


        public static Point WindowSize
        {
            get
            {
                NativeMethods.Rect wowWindowRect = ClientRect;
                return new Point(wowWindowRect.Right, wowWindowRect.Bottom);
            }
        }

        public static void Start() {
            ObjectManager.ObjectManager.Start();
            _engine.StartEngine(30);
        }

        public static void Stop() {
            ObjectManager.ObjectManager.Stop();
            _engine.StopEngine();
        }



        public static NativeMethods.Rect ClientRect => NativeMethods.GetClientRect(WindowHandle);

        public static NativeMethods.Rect ClientWindow => NativeMethods.GetWindowRect(WindowHandle);

        public static bool Initialize(int processId) {
            try {
                Process.GetProcessById(processId);
                if (!GameMemory.Open(processId)) {
                    return false;
                }

                BaseAddress = (uint) GameMemory.MainModule.BaseAddress;
                WindowHandle = Process.GetProcessById(processId).MainWindowHandle;

                return true;
            } catch (ArgumentException) {
                return false;
            } catch (Exception) {
                return false;
            }
        }
    }
}