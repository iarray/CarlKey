using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CarlKey
{
    class War3
    {
        public int ProcessID { get; set; }
        public IntPtr ProcessHandle;
        public string ModleName { get; set; }
        public string ProcessName { get; set; }
        public string Name { get; set; }
        public IntPtr ModleAddress;
        private Process _war3;

        private War3()
        {
            ModleName = "Game.dll";
            ProcessName = "War3";
            Name = "Warcraft III";
            Process[] war3 = Process.GetProcessesByName(this.ProcessName);
            if (war3.Length > 0)
            {
                ProcessID = war3[0].Id;
                ProcessHandle = war3[0].Handle;
                _war3 = war3[0];
                ModleAddress = GetModle().BaseAddress;
            }
        }

        public IntPtr WindowHandle
        {
            get
            {
                return this._war3.MainWindowHandle;
            }
        }
        public static War3 Creat()
        {
            War3 w3 = new War3();
            if (w3._war3 != null) return w3;
            else return null;
        }

        public ProcessModule GetModle()
        {
            foreach (ProcessModule Modle in _war3.Modules)
            {
                if (Modle.ModuleName == this.ModleName)
                {
                    return Modle;
                }
            }
            return null;

        }

        ////读取内存
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out int lpBuffer, int nSize, int lpNumberOfBytesRead);
        [DllImport("user32")]
        static extern int SetForegroundWindow(IntPtr hwnd);
        //[DllImport("kernel32.dll ")]
        //public static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);   
        /// <summary>
        /// 聊天地址
        /// </summary>
        /// <returns></returns>
        public bool IsTalk()
        {
            int value = 0;
            //聊天栏内存基址，值为1为打开聊天，0为关闭
            //ReadProcessMemory(ProcessHandle,0x6FAE8450, out value, 4, 0);
            ReadProcessMemory(ProcessHandle, (int)ModleAddress + 0xAE8450, out value, 4, 0);
            return value==1;
        }

        public bool IsNotCD(int x, int y, int value)
        {
            
            //Color c = PointColorPicker.GetColorFromPoint(new Point(1006, 758));
            //int v = SetForegroundWindow(this.WindowHandle);
            //int col = PointColorPicker.GetColor(this.WindowHandle,x, y);
            //Color c = Color.FromArgb(col);
            Color c =PointColorPicker.GetColorFromPoint(new Point(x, y));
            double cv =c.R*c.R+c.G*c.G+c.B*c.B;
            return cv > value;
        }
    }

    class PointColorPicker
    {
        [DllImport("gdi32.dll")]
        public static extern int GetPixel(IntPtr hwnd, int x, int y);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        private static Bitmap cache = new Bitmap(1, 1);


        private static Graphics tempGraphics = Graphics.FromImage(cache);
        /**/
        /// <summary>
        /// Gets the Color from the screen at the given point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Color GetColorFromPoint(Point point)
        {
            tempGraphics.CopyFromScreen((int)point.X, (int)point.Y, 0, 0, new Size(1, 1));
            return cache.GetPixel(0, 0);

        }

        public static int GetColor(IntPtr hwnd, int x, int y)
        {

            IntPtr dc = GetWindowDC(hwnd);
            return GetPixel(dc, x, y);
        }
    }
}
