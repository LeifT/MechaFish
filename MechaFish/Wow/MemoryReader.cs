using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MechaFish.Wow {
    public class MemoryReader : IDisposable {
        private int _processId;

        private ProcessModule _processModule;
        public ProcessModule MainModule => _processModule ?? (_processModule = Process.GetProcessById(_processId).Modules[0]);

        public bool IsOpen => _processId > 0;
        public IntPtr ProcessHandle { get; private set; }

        #region IDisposable Members

        private bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disponeManagedResources) {

            if (!_disposed) {
                if (disponeManagedResources) {
                    // Clean up managed resources
                    MainModule.Dispose();
                }

                _disposed = true;
            }

            // Clean up native resources
            if (ProcessHandle != IntPtr.Zero) {
                NativeMethods.CloseHandle(ProcessHandle);
            }
        }

        ~MemoryReader() {
            Dispose(false);
        }

        #endregion

        public bool Open(int procId) {
            if (procId == 0) {
                throw new ArgumentException(@"Proccess ID cannot be 0!", nameof(procId));
            }

            ProcessHandle = NativeMethods.OpenProcess( NativeMethods.ProcessAccess.AllAccess, false, procId);

            if (ProcessHandle == IntPtr.Zero) {
                throw new Win32Exception("Failed to open the process for reading.");
            }

            //Process.EnterDebugMode();

            _processId = procId;
            return true;
        }

        public byte[] ReadBytes(IntPtr address, long count) {
            return NativeMethods.ReadProcessMemory(ProcessHandle, address, count);
        }

        public string ReadCString(uint address, Encoding encType) {
            return ReadCString(new IntPtr(address), encType);
        }

        public string ReadCString(IntPtr address, Encoding encType) {
            // Unknown string nSize.
            var buffer = new List<byte>();

            var i = 0;
            var current = Read<byte>(address + i);

            while (current != '\0')
            {
                buffer.Add(current);
                i++;
                current = Read<byte>(address + i);
            }

            return encType.GetString(buffer.ToArray());
        }

        public byte[] ReadBytes(uint address, uint count) {
            return ReadBytes(new IntPtr(address), count);
        }

        public T Read<T>(IntPtr address) where T : struct {
            object ret;
            var buffer = new byte[0];
            
            if (!(typeof(T) == typeof(string))) {
                buffer = ReadBytes(address, (uint)Marshal.SizeOf(typeof(T)));
            } 

            switch (Type.GetTypeCode(typeof(T))) {
                case TypeCode.Object:
                    var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    ret = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                    handle.Free();
                    break;
                case TypeCode.Boolean:
                    ret = BitConverter.ToBoolean(buffer, 0);
                    break;
                case TypeCode.Char:
                    ret = BitConverter.ToChar(buffer, 0);
                    break;
                case TypeCode.Byte:
                    // Will throw an argument out of range exception if 0 bytes were read.
                    // This is on purpose!
                    ret = buffer[0];
                    break;
                case TypeCode.Int16:
                    ret = BitConverter.ToInt16(buffer, 0);
                    break;
                case TypeCode.UInt16:
                    ret = BitConverter.ToUInt16(buffer, 0);
                    break;
                case TypeCode.Int32:
                    ret = BitConverter.ToInt32(buffer, 0);
                    break;
                case TypeCode.UInt32:
                    ret = BitConverter.ToUInt32(buffer, 0);
                    break;
                case TypeCode.Int64:
                    ret = BitConverter.ToInt64(buffer, 0);
                    break;
                case TypeCode.UInt64:
                    ret = BitConverter.ToUInt64(buffer, 0);
                    break;
                case TypeCode.Single:
                    ret = BitConverter.ToSingle(buffer, 0);
                    break;
                case TypeCode.Double:
                    ret = BitConverter.ToDouble(buffer, 0);
                    break;
                case TypeCode.String:
                    ret = ReadCString(address, Encoding.UTF8);
                    break;
                default:
                    throw new NotSupportedException($"{typeof(T).FullName} is not currently supported by this function.");
            }

            return (T) ret;
        }

        public T Read<T>(uint address) where T : struct {
            return Read<T>(new IntPtr(address));
        }

        public bool Write<T>(IntPtr address, T value) {
            Marshal.StructureToPtr(value, address, false);
            return true;
        }

        public void Write(IntPtr address, byte[] pBytes) {
            IntPtr bytesWritten;
            NativeMethods.WriteProcessMemory(ProcessHandle, address, pBytes, new IntPtr(pBytes.Length), out bytesWritten);
        }
    }
}