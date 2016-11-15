using System;

namespace MasterAngler.Wow.Utils {
    [Serializable]
    public struct Matrix {
        public static Matrix Identity => new Matrix
        {
            M11 = 1f,
            M22 = 1f,
            M33 = 1f,
            M44 = 1f
        };

        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;

        #region Constructor
        
        public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31,
            float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates a right-handed look-at matrix.
        /// </summary>
        public static Matrix LookAtRh(Vector3 camera, Vector3 target, Vector3 up) {
            var zaxis = (camera - target).Normalized;
            var xaxis = Vector3.Cross(up, zaxis).Normalized;
            var yaxis = Vector3.Cross(zaxis, xaxis);

            var result = new Matrix {
                M11 = xaxis.X,
                M21 = xaxis.Y,
                M31 = xaxis.Z,
                M41 = -Vector3.Dot(xaxis, camera),
                M12 = yaxis.X,
                M22 = yaxis.Y,
                M32 = yaxis.Z,
                M42 = -Vector3.Dot(yaxis, camera),
                M13 = zaxis.X,
                M23 = zaxis.Y,
                M33 = zaxis.Z,
                M43 = -Vector3.Dot(zaxis, camera),
                M14 = 0,
                M24 = 0,
                M34 = 0,
                M44 = 1
            };

            return result;
        }

        /// <summary>
        ///     Creates a right-handed perspective projection matrix.
        /// </summary>
        public static Matrix PerspectiveFovRh(float fieldOfViewY, float aspectRatio, float znearPlane, float zfarPlane) {
            var height = (float)(1f / Math.Tan(fieldOfViewY / 2f));
            var width = height / aspectRatio;

            return new Matrix {
                M11 = width,
                M22 = height,
                M33 = zfarPlane / (znearPlane - zfarPlane),
                M34 = -1f,
                M43 = znearPlane * zfarPlane / (znearPlane - zfarPlane)
            };
        }

        #endregion
        
        #region Operators

        /// <summary>
        ///     Multiplies two matrices.
        /// </summary>
        public static Matrix operator *(Matrix lhs, Matrix rhs) {
            return new Matrix  {
                M11 = lhs.M11 * rhs.M11 + lhs.M12 * rhs.M21 + lhs.M13 * rhs.M31 + lhs.M14 * rhs.M41,
                M12 = lhs.M11 * rhs.M12 + lhs.M12 * rhs.M22 + lhs.M13 * rhs.M32 + lhs.M14 * rhs.M42,
                M13 = lhs.M11 * rhs.M13 + lhs.M12 * rhs.M23 + lhs.M13 * rhs.M33 + lhs.M14 * rhs.M43,
                M14 = lhs.M11 * rhs.M14 + lhs.M12 * rhs.M24 + lhs.M13 * rhs.M34 + lhs.M14 * rhs.M44,
                M21 = lhs.M21 * rhs.M11 + lhs.M22 * rhs.M21 + lhs.M23 * rhs.M31 + lhs.M24 * rhs.M41,
                M22 = lhs.M21 * rhs.M12 + lhs.M22 * rhs.M22 + lhs.M23 * rhs.M32 + lhs.M24 * rhs.M42,
                M23 = lhs.M21 * rhs.M13 + lhs.M22 * rhs.M23 + lhs.M23 * rhs.M33 + lhs.M24 * rhs.M43,
                M24 = lhs.M21 * rhs.M14 + lhs.M22 * rhs.M24 + lhs.M23 * rhs.M34 + lhs.M24 * rhs.M44,
                M31 = lhs.M31 * rhs.M11 + lhs.M32 * rhs.M21 + lhs.M33 * rhs.M31 + lhs.M34 * rhs.M41,
                M32 = lhs.M31 * rhs.M12 + lhs.M32 * rhs.M22 + lhs.M33 * rhs.M32 + lhs.M34 * rhs.M42,
                M33 = lhs.M31 * rhs.M13 + lhs.M32 * rhs.M23 + lhs.M33 * rhs.M33 + lhs.M34 * rhs.M43,
                M34 = lhs.M31 * rhs.M14 + lhs.M32 * rhs.M24 + lhs.M33 * rhs.M34 + lhs.M34 * rhs.M44,
                M41 = lhs.M41 * rhs.M11 + lhs.M42 * rhs.M21 + lhs.M43 * rhs.M31 + lhs.M44 * rhs.M41,
                M42 = lhs.M41 * rhs.M12 + lhs.M42 * rhs.M22 + lhs.M43 * rhs.M32 + lhs.M44 * rhs.M42,
                M43 = lhs.M41 * rhs.M13 + lhs.M42 * rhs.M23 + lhs.M43 * rhs.M33 + lhs.M44 * rhs.M43,
                M44 = lhs.M41 * rhs.M14 + lhs.M42 * rhs.M24 + lhs.M43 * rhs.M34 + lhs.M44 * rhs.M44
            };
        }

        #endregion
    }
}