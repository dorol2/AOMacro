using System;
using System.Runtime.InteropServices;

namespace ALMarcro.Core.Kernel
{
    static class User32
    {
        public static class MouseEventControl
        {
            public const int WM_LBUTTONDOWN = 0x201;
            public const int WM_LBUTTONUP = 0x202;
        }

        [DllImport(nameof(User32), EntryPoint = nameof(FindWindow))]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(nameof(User32), EntryPoint = nameof(PrintWindow))]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [DllImport(nameof(User32), EntryPoint = nameof(SendMessage))]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        [DllImport(nameof(User32), EntryPoint = nameof(FindWindowEx))]
        public static extern IntPtr FindWindowEx(IntPtr Parent, IntPtr Child, string lpszClass, string lpszWindows);
    }
}
