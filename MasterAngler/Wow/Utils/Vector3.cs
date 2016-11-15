using System;

namespace MasterAngler.Wow.Utils {
    public struct Vector3 {
        public float X;
        public float Y;
        public float Z;

        /// <summary>
        ///     Returns the length of this vector (Read Only).
        /// </summary>
        public float Magnitude => (float) Math.Sqrt(SqrMagnitude);

        /// <summary>
        ///     Returns the squared length of this vector (Read Only).
        /// </summary>
        public float SqrMagnitude => X*X + Y*Y + Z*Z;

        /// <summary>
        ///     Returns this vector with a length of 1 (Read Only).
        /// </summary>
        public Vector3 Normalized {
            get {
                var len = Magnitude;
                return new Vector3(X/len, Y/len, Z/len);
            }
        }

        public static Vector3 Back => new Vector3(-1f, 0f, 0f);
        public static Vector3 Down => new Vector3(0f, 0f, -1f);
        public static Vector3 Forward => new Vector3(1f, 0f, 0f);
        public static Vector3 Left => new Vector3(0f, 1f, 0f);
        public static Vector3 One => new Vector3(1f, 1f, 1f);
        public static Vector3 Right => new Vector3(0f, -1f, 0f);
        public static Vector3 Up => new Vector3(0f, 0f, 1f);
        public static Vector3 Zero => new Vector3(0f, 0f, 0f);

        #region Constructor

        public Vector3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Returns a deep copy of this vector.
        /// </summary>
        public Vector3 Clone() {
            return new Vector3(X, Y, Z);
        }

        /// <summary>
        ///     Makes this vector have a magnitude of 1.
        /// </summary>
        public void Normalize() {
            var len = Magnitude;
            Set(X/len, Y/len, Z/len);
        }

        /// <summary>
        ///     Set the x, y and z components for this vector.
        /// </summary>
        public void Set(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Returns a formatted string for this vector.
        /// </summary>
        public override string ToString() {
            return $"Vector3 ({X:0.##}, {Y:0.##}, {Z:0.##})";
        }

        public bool Equals(Vector3 other) {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }

            return obj is Vector3 && Equals((Vector3)obj);
        }

        /// <summary>
        ///     Returns the hash code for this vector.
        /// </summary>
        public override int GetHashCode() {
            unchecked {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        ///     Returns the Cross Product of two vectors.   
        /// </summary>
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs) {
            return new Vector3(lhs.Y*rhs.Z - lhs.Z*rhs.Y, lhs.Z*rhs.X - lhs.X*rhs.Z, lhs.X*rhs.Y - lhs.Y*rhs.X);
        }   

        /// <summary>
        ///     Returns the Dot Product of two vectors.
        /// </summary>
        public static float Dot(Vector3 lhs, Vector3 rhs) {
            return lhs.X*rhs.X + lhs.Y*rhs.Y + lhs.Z*rhs.Z;
        }

        /// <summary>
        ///     Projects a vector from object space into screen space.
        /// </summary>
        public static Vector3 Project(Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix worldViewProjection) {
            var vector3 = vector * worldViewProjection;
            return new Vector3((1f + vector3.X)*0.5f*width + x, (1f - vector3.Y)*0.5f*height + y, vector3.Z*(maxZ - minZ) + minZ);
        }

        #endregion

        #region Operators

        /// <summary>
        ///     Add two vectors.
        /// </summary>
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs) {
            return new Vector3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        /// <summary>
        ///     Subtract two vectors.
        /// </summary>
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs) {
            return new Vector3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        /// <summary>
        ///     Returns true if the vectors are equal.
        /// </summary>
        public static bool operator ==(Vector3 lhs, Vector3 rhs) {
            return (lhs - rhs).SqrMagnitude < float.Epsilon;
        }

        /// <summary>
        ///     Returns true if the vectors are different.
        /// </summary>
        public static bool operator !=(Vector3 lhs, Vector3 rhs) {
            return !(lhs == rhs);
        }

        /// <summary>
        ///     Multiplies two vectors.
        /// </summary>
        public static Vector3 operator *(Vector3 lhs, Vector3 rhs) {
            return new Vector3(lhs.X*rhs.X, lhs.Y*rhs.Y, lhs.Z*rhs.Z);
        }

        /// <summary>
        ///     Multiplies a vector by a number.
        /// </summary>
        public static Vector3 operator *(Vector3 lhs, float rhs) {
            return new Vector3(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
        }

        /// <summary>
        ///     Multiplies a vector by a matrix.
        /// </summary>
        public static Vector3 operator *(Vector3 lhs, Matrix rhs) {
            var vector3 = new Vector3 {
                X = lhs.X * rhs.M11 + lhs.Y * rhs.M21 + lhs.Z * rhs.M31 + rhs.M41,
                Y = lhs.X * rhs.M12 + lhs.Y * rhs.M22 + lhs.Z * rhs.M32 + rhs.M42,
                Z = lhs.X * rhs.M13 + lhs.Y * rhs.M23 + lhs.Z * rhs.M33 + rhs.M43
            };

            var x = 1f / (lhs.X * rhs.M14 + lhs.Y * rhs.M24 + lhs.Z * rhs.M34 + rhs.M44);
            return new Vector3(vector3.X * x, vector3.Y * x, vector3.Z * x);
        }

        #endregion
    }
}