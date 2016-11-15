using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MasterAngler.Wow.Patch;
using MasterAngler.Wow.Utils;

namespace MasterAngler.Wow.ObjectManager {
    public static class ObjectManager {
        private static Dictionary<WowGuid, WowObject> _enteties;
        public static readonly WowLocalPlayer LocalPlayer;
        private static CancellationTokenSource _cts = new CancellationTokenSource();
        private static CancellationToken _token;

        public static WowObject MouseOverObject {
            get {

                var guid = Memory.GameMemory.ReadBytes(Memory.BaseAddress + Addresses.Player.MouseOverGuid, WowGuid.Size);
                return GetObject<WowObject>(new WowGuid(guid));
            }
        }

        public static WowUnit TargetObject {
            get {


                var guid = Memory.GameMemory.ReadBytes(Memory.BaseAddress + Addresses.Player.TargetGuid, WowGuid.Size);
                var b = GetObject<WowUnit>(new WowGuid(guid));
                return b;
            }
        }

        static ObjectManager() {
            _enteties = new Dictionary<WowGuid, WowObject>();
            Dump();
            WowObject.SetMouseStrategy(new BackgroundMouse());
            LocalPlayer = new WowLocalPlayer(Memory.GameMemory.Read<uint>(Memory.BaseAddress + Addresses.Player.LocalPlayer));
        }

        public static T GetObject<T>(WowGuid guid) where T : WowObject, new(){
            WowObject wowObject;

            if (!_enteties.TryGetValue(guid, out wowObject)) {
                return new T();
            }

            return wowObject as T;
        }
 
        public static void Dump() {
            var wowObjects = new Dictionary<WowGuid, WowObject>();

            var list = Memory.GameMemory.Read<uint>(Memory.BaseAddress + Addresses.ObjectManager.EntitiyList);

            if (list != 0) {
                var objectPointer = Memory.GameMemory.Read<uint>(list + Addresses.ObjectManager.FirstEntity);

                while (((objectPointer & 1) == 0) && (objectPointer != 0)) {
                    var objectGuid = new WowGuid(Memory.GameMemory.ReadBytes(objectPointer + Addresses.ObjectManager.Guid, WowGuid.Size));
                    var objectType = (WowObject.WowObjectType) Memory.GameMemory.Read<uint>(objectPointer + Addresses.Entity.Type);

                    if (!wowObjects.ContainsKey(objectGuid)) {
                        if (!_enteties.ContainsKey(objectGuid)) {
                            wowObjects.Add(objectGuid, GetNewObject(objectType, objectPointer));
                        } else {
                            wowObjects.Add(objectGuid, _enteties[objectGuid]);
                        }
                    }

                    objectPointer = Memory.GameMemory.Read<uint>(objectPointer + Addresses.ObjectManager.NextEntity);
                }
            }
            _enteties = wowObjects;
        }

        private static WowObject GetNewObject(WowObject.WowObjectType type, uint address) {
            
            switch (type) {
                case WowObject.WowObjectType.Container: {
                    return new WowContainer(address);
                }
                case WowObject.WowObjectType.GameObject: {
                    return new WowGameObject(address);
                }
                case WowObject.WowObjectType.Item: {
                    return new WowItem(address);
                }
                case WowObject.WowObjectType.Unit: {
                        return new WowUnit(address);
                }
                default: {
                    return new WowObject(address);
                }
            }
        }

        internal static void Start() {
            _cts = new CancellationTokenSource();
            _token = _cts.Token;

            Task.Run(async () => {
                while (true) {
                    Dump();

                    await Task.Delay(200, _token).ContinueWith(tsk => { });

                    if (_cts.IsCancellationRequested) {
                        break;
                    }
                }
            }, _token);
        }

        internal static void Stop() {
            _cts.Cancel();
            _cts.Dispose();
        }

        public static List<T> GetObjectsOfType<T>() where T : WowObject {
            return _enteties.Values.OfType<T>().ToList();
        }
    }
}