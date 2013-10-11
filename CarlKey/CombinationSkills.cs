using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CarlKey
{
    class CombinationSkills
    {
        //获取窗口句柄传递键盘消息
        [DllImport("USER32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int iParam);

        private List<Keys[]> skillColl;
        public readonly Keys[] _keys;
        private War3 warIII;
        private HoldingSkills holdskils = new HoldingSkills();
        public bool IsTalk;
        public GameResolution gameInfo;
        public int Threshold;

        public CombinationSkills()
        {
            skillColl = new List<Keys[]>();
            skillColl.Add(new Keys[3] { Keys.Q, Keys.Q, Keys.Q });
            skillColl.Add(new Keys[3] { Keys.Q, Keys.Q, Keys.W });
            skillColl.Add(new Keys[3] { Keys.Q, Keys.W, Keys.W });
            skillColl.Add(new Keys[3] { Keys.Q, Keys.W, Keys.E });
            skillColl.Add(new Keys[3] { Keys.W, Keys.W, Keys.W });
            skillColl.Add(new Keys[3] { Keys.W, Keys.W, Keys.E });
            skillColl.Add(new Keys[3] { Keys.W, Keys.E, Keys.E });
            skillColl.Add(new Keys[3] { Keys.E, Keys.E, Keys.E });
            skillColl.Add(new Keys[3] { Keys.E, Keys.Q, Keys.Q });
            skillColl.Add(new Keys[3] { Keys.E, Keys.E, Keys.Q });
            _keys = new Keys[10] { Keys.Y, Keys.V, Keys.X, Keys.B, Keys.C, Keys.Z, Keys.D, Keys.T, Keys.G, Keys.F };
            gameInfo = new GameResolution();
        }

        public bool CombinateSkill(Skills sks, int index)
        {
            warIII = War3.Creat();
            if (warIII != null && warIII.IsNotCD(this.gameInfo.CDPointX,this.gameInfo.CDPointY,this.Threshold) &&     
                holdskils.IsNotExitAndAdd(warIII,index,this.gameInfo) 
                && (IsTalk=warIII.IsTalk() )== false)
            {
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYDOWN, (int)(skillColl[index][0]), 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYUP, (int)(skillColl[index][0]), 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYDOWN, (int)(skillColl[index][1]), 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYUP, (int)(skillColl[index][1]), 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYDOWN, (int)(skillColl[index][2]), 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYUP, (int)(skillColl[index][2]), 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYDOWN, (int)Keys.R, 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYUP, (int)Keys.R, 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYDOWN,(int)sks.SpheresCollection[index][0],0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYUP, (int)sks.SpheresCollection[index][0], 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYDOWN,(int)sks.SpheresCollection[index][1],0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYUP, (int)sks.SpheresCollection[index][1], 0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYDOWN,(int)sks.SpheresCollection[index][2],0);
                SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYUP, (int)sks.SpheresCollection[index][2], 0);

                return true;
            }
            return false;
        }

        public void SendKeyToWar3(params Keys[] keys)
        {
            warIII = War3.Creat();
            if (warIII != null)
            {
                foreach (Keys key in keys)
                {
                    SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYDOWN, (int)key, 0);
                    SendMessage(warIII.WindowHandle, (uint)Hook.WM_KEYBOARD.WM_KEYUP, (int)key, 0);
                }
            }
        }
    }
}
