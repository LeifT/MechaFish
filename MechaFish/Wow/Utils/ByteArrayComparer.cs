using System;
using System.Collections.Generic;
using System.Linq;

namespace MechaFish.Wow.Utils {
    public class ByteArrayComparer : IEqualityComparer<byte[]> {
        public bool Equals(byte[] a, byte[] b) {
            return a.SequenceEqual(b);
        }

        public int GetHashCode(byte[] key) {
            if (key == null) {
                throw new ArgumentNullException(nameof(key));
            }

            return key.Sum(b => b);
        }
    }
}