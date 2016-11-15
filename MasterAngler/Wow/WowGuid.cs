using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MasterAngler.Wow {
    public class WowGuid : IEnumerable<byte> {
        public static uint Size = 16;
        private readonly byte[] _guid;

        public WowGuid(byte[] guid) {
            if (guid.Length != Size) {
                throw new ArgumentException($"wowGuid should be byte[{Size}]");
            }

            _guid = guid;
        }

        public WowGuid() {
            _guid = new byte[Size];
        }

        public byte this[int index]
        {
            get { return _guid[index]; }
            set { _guid[index] = value; }
        }

        public byte[] ToArray() {
            return _guid;
        }

        public override bool Equals(object obj) {
            var wowGuid = obj as WowGuid;

            return (wowGuid != null) && _guid.SequenceEqual(wowGuid._guid);
        }

        public bool Equals(WowGuid wowGuid) {
            return (wowGuid != null) && _guid.SequenceEqual(wowGuid._guid);
        }

        public bool IsEmpty {

            get {
                foreach (byte t in _guid) {
                    if (t != 0) {
                        return false;
                    }
                }

                return true;
            }
        }

        public static bool operator ==(WowGuid lhs, WowGuid rhs) {
            if (ReferenceEquals(lhs, rhs)) {
                return true;
            }

            if (((object) lhs == null) || ((object) rhs == null)) {
                return false;
            }

            return lhs._guid.SequenceEqual(rhs._guid);
        }

        public static bool operator !=(WowGuid lhs, WowGuid rhs) {
            return !(lhs == rhs);
        }

        public override int GetHashCode() {
            return _guid.Sum(b => b);
        }

        IEnumerable<byte> CreateEnumerable()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return _guid[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<byte> GetEnumerator() {
            return CreateEnumerable().GetEnumerator();
        }

        public override string ToString() {
            return _guid.Aggregate("", (current, b) => current + $"{b:X2} ");
        }
    }
}