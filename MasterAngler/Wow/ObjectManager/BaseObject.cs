namespace MasterAngler.Wow.ObjectManager {
    public abstract class BaseObject {
        public bool IsValid => Pointer != 0;

        public uint Pointer {
            get;
            private set;
        }

        protected BaseObject(uint pointer) {
            Pointer = pointer;
        }


        public void SetPointer(uint pointer) {
            Pointer = pointer;
        }
    }
}