using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MechaFish.Wow {
    public static class NativeMethods {
        public static IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam) {
            return TrySendMessageNative(hWnd, msg, wParam, lParam);
        }

        public static uint GetWindowThreadProcessId(IntPtr hWnd) {
            uint processId;
            TryGetWindowThreadProcessIdNative(hWnd, out processId);
            return processId;
        }

        public static IntPtr GetForegroundWindow() {
            return TryGetForegroundWindowNative();
        }

        public static Rect GetWindowRect(IntPtr hWnd)
        {
            Rect lpRect;
            TryGetWindowRectNative(hWnd, out lpRect);
            return lpRect;
        }

        public static Rect GetClientRect(IntPtr hWnd) {
            Rect lpRect;
            TryGetClientRectNative(hWnd, out lpRect);
            return lpRect;
        }

        public static Point ClientToScreen(IntPtr hwnd, int x, int y) {
            Point lpPoint = new Point(x, y);

            if (hwnd != IntPtr.Zero) {
                TryClientToScreenNative(hwnd, ref lpPoint);
            }
            
            return lpPoint;
        }

        public static Point ScreenToClient(IntPtr hwnd, int x, int y)
        {
            Point lpPoint = new Point(x, y);

            if (hwnd != IntPtr.Zero)
            {
                TryScreenToClientNative(hwnd, ref lpPoint);
            }

            return lpPoint;
        }

        public static bool SetCursorPos(int x, int y) {
            return TrySetCursorPosNative(x, y);
        }
        
        public static byte[] ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, long nSize) {
            var buffer = new byte[nSize];
            IntPtr bytesRead;

            if (!TryReadProcessMemoryNative(hProcess, lpBaseAddress, buffer, new IntPtr(nSize), out bytesRead)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (bytesRead.ToInt64() != nSize) {
                throw new Exception("Bytes read did not match required byte count!");
            }

            return buffer;

        }
        
        public static bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesWritten) {
            return TryWriteProcessMemoryNative(hProcess, lpBaseAddress, lpBuffer, nSize, out lpNumberOfBytesWritten);
        }
        
        public static IntPtr OpenProcess(ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId) {
            return TryOpenProcessNative(dwDesiredAccess, bInheritHandle, dwProcessId);
        }
        
        public static bool CloseHandle(IntPtr hObject) {
            if (!TryCloseHandleNative(hObject)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return true;
        }

        public static Point GetCursorPos() {
            Point p = new Point();
            TryGetCursorPosNative(ref p);
            return p;
        }

        #region Nested type: ProcessAccess

        [Flags]
        public enum ProcessAccess
        {
            /// <summary>Specifies all possible access flags for the process object.</summary>
            AllAccess =
                CreateThread | DuplicateHandle | QueryInformation | SetInformation | Terminate | VMOperation | VMRead |
                VMWrite | Synchronize,

            /// <summary>Enables usage of the process handle in the CreateRemoteThread function to create a thread in the process.</summary>
            CreateThread = 0x2,

            /// <summary>
            ///     Enables usage of the process handle as either the source or target process in the DuplicateHandle function to
            ///     duplicate a handle.
            /// </summary>
            DuplicateHandle = 0x40,

            /// <summary>
            ///     Enables usage of the process handle in the GetExitCodeProcess and GetPriorityClass functions to read
            ///     information from the process object.
            /// </summary>
            QueryInformation = 0x400,

            /// <summary>Enables usage of the process handle in the SetPriorityClass function to set the priority class of the process.</summary>
            SetInformation = 0x200,

            /// <summary>Enables usage of the process handle in the TerminateProcess function to terminate the process.</summary>
            Terminate = 0x1,

            /// <summary>
            ///     Enables usage of the process handle in the VirtualProtectEx and WriteProcessMemory functions to modify the
            ///     virtual memory of the process.
            /// </summary>
            VMOperation = 0x8,

            /// <summary>
            ///     Enables usage of the process handle in the ReadProcessMemory function to' read from the virtual memory of the
            ///     process.
            /// </summary>
            VMRead = 0x10,

            /// <summary>
            ///     Enables usage of the process handle in the WriteProcessMemory function to write to the virtual memory of the
            ///     process.
            /// </summary>
            VMWrite = 0x20,

            /// <summary>Enables usage of the process handle in any of the wait functions to wait for the process to terminate.</summary>
            Synchronize = 0x100000
        }

        #endregion

        #region PInvokes

        [DllImport("user32.dll", EntryPoint = "ScreenToClient")]
        private static extern bool TryScreenToClientNative(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "ClientToScreen", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool TryClientToScreenNative(IntPtr hwnd, ref Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        private static extern bool TryGetCursorPosNative(ref Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, ExactSpelling = false)]
        private static extern IntPtr TrySendMessageNative(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        private static extern uint TryGetWindowThreadProcessIdNative(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static extern IntPtr TryGetForegroundWindowNative();

        [DllImport("user32.dll", EntryPoint = "GetClientRect", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool TryGetClientRectNative(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        private static extern bool TryGetWindowRectNative(IntPtr hwnd, out Rect rectangle);



        [DllImport("user32.dll", EntryPoint = "SetCursorPos", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool TrySetCursorPosNative(int x, int y);

        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory", SetLastError = true, PreserveSig = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool TryReadProcessMemoryNative(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        private static extern bool TryWriteProcessMemoryNative(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        private static extern IntPtr TryOpenProcessNative(ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", EntryPoint = "CloseHandle", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool TryCloseHandleNative(IntPtr hObject);

        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}