using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MechaFish.Wow.Patch;

namespace MechaFish.Wow.ObjectManager {
    public static class ObjectManager {
        private static Dictionary<WowGuid, WowObject> _enteties;
        public static readonly WowLocalPlayer LocalPlayer;
        private static CancellationTokenSource _cts = new CancellationTokenSource();
        private static CancellationToken _token;

        public static WowObject MouseOverObject {
            get {
                var guid = GameManager.GameMemory.ReadBytes(GameManager.BaseAddress + Addresses.Player.MouseOverGuid, WowGuid.Size);
                return GetObject<WowObject>(new WowGuid(guid));
            }
        }

        public static WowUnit TargetObject {
            get {
                var guid = GameManager.GameMemory.ReadBytes(GameManager.BaseAddress + Addresses.Player.TargetGuid, WowGuid.Size);
                return GetObject<WowUnit>(new WowGuid(guid));
            }
        }

        static ObjectManager() {
            _enteties = new Dictionary<WowGuid, WowObject>();
            CollectEntities();

            LocalPlayer = new WowLocalPlayer(GameManager.GameMemory.Read<uint>(GameManager.BaseAddress + Addresses.Player.LocalPlayer));
        }

        public static T GetObject<T>(WowGuid guid) where T : WowObject, new(){
            WowObject wowObject;

            if (!_enteties.TryGetValue(guid, out wowObject)) {
                return new T();
            }

            return wowObject as T;
        }
 
        public static void CollectEntities() {
            var wowObjects = new Dictionary<WowGuid, WowObject>();

            var list = GameManager.GameMemory.Read<uint>(GameManager.BaseAddress + Addresses.ObjectManager.EntitiyList);
            
            if (list != 0) {
                var objectPointer = GameManager.GameMemory.Read<uint>(list + Addresses.ObjectManager.FirstEntity);

                while (((objectPointer & 1) == 0) && (objectPointer != 0)) {
                    var objectGuid = new WowGuid(GameManager.GameMemory.ReadBytes(objectPointer + Addresses.ObjectManager.Guid, WowGuid.Size));
                    
                    if (!wowObjects.ContainsKey(objectGuid)) {
                        if (_enteties.ContainsKey(objectGuid)) {
                            wowObjects.Add(objectGuid, _enteties[objectGuid]);
                        } else {
                            wowObjects.Add(objectGuid, GetNewObject(objectPointer));
                        }
                    }
                    
                    objectPointer = GameManager.GameMemory.Read<uint>(objectPointer + Addresses.ObjectManager.NextEntity);
                }
            }
            _enteties = wowObjects;
        }

        private static WowObject GetNewObject(uint address) {
            var objectType = (WowObject.WowObjectType)GameManager.GameMemory.Read<uint>(address + Addresses.Entity.Type);

            switch (objectType) {
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
                    CollectEntities();

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