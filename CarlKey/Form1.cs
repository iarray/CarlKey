using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CarlKey
{
    public partial class Form1 : Form
    {

        private Skills skills;
        Hook.KeyBoardHook keybdHook;
        private CombinationSkills combSkills;


        public Form1()
        {
            InitializeComponent();
            InitializeData();
            LoadControls();
            combSkills = new CombinationSkills() { Threshold =(int)( numThreshold.Value * numThreshold.Value*3 )};
            keybdHook = new Hook.KeyBoardHook();
            keybdHook.KeyDown += keybdHook_KeyDown;
        }
        #region 键盘钩子处理事件
        bool keybdHook_KeyDown(object sender, Hook.KeyBoardHookEventArgs e)
        {
            int index = Array.IndexOf(skills.ShortcutsKeyCode,(Keys)e.Key);
            if (index!=-1)
            {
                if (combSkills.CombinateSkill(skills,index))
                {
                    if (skills.IsAutoCastSkills[index])
                    {
                        Thread.Sleep(200);
                        combSkills.SendKeyToWar3(combSkills._keys[index]);
                    }
                    return true;
                }
                else
                {
                    if (combSkills.IsTalk)
                    {
                        combSkills.SendKeyToWar3((Keys)e.Key);
                    }
                    else
                    {
                        combSkills.SendKeyToWar3(combSkills._keys[index]);
                    }
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region 控件加载
        public void LoadControls()
        {
            for (int i = 0, x = 8, y = 30; i < 10; i++, y += 30)
            {
                Label lab = new Label();
                lab.Text = skills.AllSkill[i];
                lab.Location = new Point(x, y);
                lab.Width = 100;
                lab.Name = "lbSkill" + i.ToString();
                TextBox tb = new TextBox() { Width = 70, TabStop = false, BackColor = Color.WhiteSmoke, ReadOnly = true };
                tb.Text = skills.Shortcuts[i];
                tb.Location = new Point(x + 120, y);
                tb.Name = "tbSkill" + i.ToString();
                tb.KeyDown += tb_KeyDown;
                Label splab = new Label() { Text = "自动法球",Location=new Point(x+200,y+3),Width=60};
                TextBox tbsps1 = new TextBox()
                {
                    Width = 30,
                    TabStop = false,
                    BackColor = Color.WhiteSmoke,
                    ReadOnly = true,
                    Location = new Point(x + 270, y),
                    Name="lbsps"+i.ToString(),
                    Text=skills.SpheresCollection[i][0].ToString()
                };
                tbsps1.KeyDown += tbsps1_KeyDown;
                Label addlab = new Label() { Text = "+", Location = new Point(x + 305, y + 3), Width = 10 };
                TextBox tbsps2 = new TextBox()
                {
                    Width = 30,
                    TabStop = false,
                    BackColor = Color.WhiteSmoke,
                    ReadOnly = true,
                    Location = new Point(x + 320, y),
                    Name = "lbsps" + i.ToString(),
                    Text = skills.SpheresCollection[i][1].ToString()
                };
                tbsps2.KeyDown += tbsps2_KeyDown;
                Label addlab2 = new Label() { Text = "+", Location = new Point(x + 355, y + 3), Width = 10 };
                TextBox tbsps3 = new TextBox()
                {
                    Width = 30,
                    TabStop = false,
                    BackColor = Color.WhiteSmoke,
                    ReadOnly = true,
                    Location = new Point(x + 370, y),
                    Name = "lbsps" + i.ToString(),
                    Text = skills.SpheresCollection[i][2].ToString()
                };
                tbsps3.KeyDown += tbsps3_KeyDown;
                CheckBox cbAutoSkill = new CheckBox()
                {
                    Text="立即施放",
                    TabStop = false,
                    Name="cbAutoSkill"+i.ToString(),
                    Location=new Point(x+405,y-3),
                    Width=85,
                    Checked=skills.IsAutoCastSkills[i]
                };
                cbAutoSkill.CheckedChanged += cbAutoSkill_CheckedChanged;
                this.ControlGroupBox.Controls.Add(lab);
                this.ControlGroupBox.Controls.Add(lab);
                this.ControlGroupBox.Controls.Add(tb);
                this.ControlGroupBox.Controls.Add(splab);
                this.ControlGroupBox.Controls.Add(tbsps1);
                this.ControlGroupBox.Controls.Add(addlab);
                this.ControlGroupBox.Controls.Add(tbsps2);
                this.ControlGroupBox.Controls.Add(addlab2);
                this.ControlGroupBox.Controls.Add(tbsps3);
                this.ControlGroupBox.Controls.Add(cbAutoSkill);


                //this.Controls.Add(lab);
                //this.Controls.Add(tb);
                //this.Controls.Add(splab);
                //this.Controls.Add(tbsps1);
                //this.Controls.Add(addlab);
                //this.Controls.Add(tbsps2);
                //this.Controls.Add(addlab2);
                //this.Controls.Add(tbsps3);
                //this.Controls.Add(cbAutoSkill);
            }
        }
        #endregion

        #region 控件事件
        void cbAutoSkill_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int index = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
            skills.IsAutoCastSkills[index] = cb.Checked;
        }


        void tbsps3_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            int index = int.Parse(txtBox.Name.Substring(txtBox.Name.Length - 1));
            if (e.KeyCode == Keys.Q || e.KeyCode == Keys.W || e.KeyCode == Keys.E)
            {
                txtBox.Text = e.KeyCode.ToString();
                skills.SpheresCollection[index][2] = e.KeyCode;
            }
        }

        void tbsps2_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            int index = int.Parse(txtBox.Name.Substring(txtBox.Name.Length - 1));
            if (e.KeyCode == Keys.Q || e.KeyCode == Keys.W || e.KeyCode == Keys.E)
            {
                txtBox.Text = e.KeyCode.ToString();
                skills.SpheresCollection[index][1] = e.KeyCode;
            }
        }

        void tbsps1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            int index = int.Parse(txtBox.Name.Substring(txtBox.Name.Length - 1));
            if (e.KeyCode == Keys.Q || e.KeyCode == Keys.W || e.KeyCode == Keys.E)
            {
                txtBox.Text = e.KeyCode.ToString();
                skills.SpheresCollection[index][0] = e.KeyCode;
            }
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            int index = int.Parse(txtBox.Name.Substring(txtBox.Name.Length - 1));
            if (e.KeyCode != Keys.Back)
            {
                txtBox.Text = e.KeyCode.ToString();
                skills.Shortcuts[index] = txtBox.Text;
                skills.ShortcutsKeyCode[index] = e.KeyCode;
            }
            else
            {
                txtBox.Text = string.Empty;
                skills.Shortcuts[index] = string.Empty;
                skills.ShortcutsKeyCode[index] = Keys.None;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (keybdHook != null)
            {
                keybdHook.Dispose();
            }
            skills.SaveData();
        }

        private void numThreshold_ValueChanged(object sender, EventArgs e)
        {
            skills.Threshold = numThreshold.Value;
            if (combSkills != null)
            {
                combSkills.Threshold = (int)(numThreshold.Value * numThreshold.Value * 3);
            }
        }
        #endregion

        #region 初始化控件数据
        public void InitializeData()
        {
            if (File.Exists("Keys.xml") == true)
            {
                skills = Skills.CreatFromXmlFile("Keys.xml");

            }
            else
            {
                skills = new Skills();
            }

            numThreshold.Value = skills.Threshold;
        }
        #endregion

        #region 托盘图标事件
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SuperKael_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Hide();
        }
        #endregion


    }
}
