using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CarlKey
{
    class HoldingSkills
    {
        private int[] _skills;
        private int index;
        public HoldingSkills()
        {
            _skills = new int[2] { -1, -1 };
            index = 0;
        }

        public void ADD(int no)
        {
            if (index >1)
            {
                index = 0;
            }
            _skills[index++] = no;
        }

        private void ADD(War3 war3 , int no,GameResolution grl)
        {
            if (!HasASkill(war3.WindowHandle,grl.SecondSkillPointX,grl.SecondSkillPointY))
            {
                this.ADD(no);
            }
            else
            {
                _skills[0] = no;
                _skills[1] = no;
            }
        }

        public bool HasASkill(IntPtr hwnd,int x,int y)
        {
            int c = PointColorPicker.GetColor(hwnd,x, y);
            return c == 0;
        }

        public bool IsNotExitAndAdd(War3 war3 , int no,GameResolution gr) 
        {
            if (no != _skills[0] && no != _skills[1])
            {
                this.ADD(war3,no,gr);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Contain(int no)
        {
            return no == _skills[0] || no == _skills[1];
        }
    }
}
