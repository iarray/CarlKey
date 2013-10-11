using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CarlKey
{
    [Serializable]
    public class Skills
    {
        public readonly string[] AllSkill;
        public string[] Shortcuts;
        public Keys[] ShortcutsKeyCode;
        public const int Count = 10;
        public Spheres[] SpheresCollection;
        public bool[] IsAutoCastSkills;
        public decimal Threshold;

        public Skills()
        {
            AllSkill = new string[10] { "急速冷却(QQQ)", "幽冥漫步(QQW)", "强袭飓风(QWW)","超震声波(QWE)","电磁脉冲(WWW)"
                                       ,"灵动迅捷(WWE)","浑沌陨石(WEE)","阳炎冲击(EEE)","寒冰之墙(EQQ)","熔炉精灵(EEQ)"};
            SetDefaultValue();
        }

        public void SetDefaultValue()
        {
            Shortcuts = new string[10] { "Y", "V", "X", "B", "C", "Z", "D", "T", "G", "F" };

            ShortcutsKeyCode = new Keys[10] { Keys.Y, Keys.V, Keys.X, Keys.B, Keys.C, Keys.Z, Keys.D, Keys.T, Keys.G,                                                    Keys.F };
            SpheresCollection = new Spheres[10]{new Spheres(),new Spheres(Keys.W),new Spheres(Keys.W),new Spheres(),
                new Spheres(),new Spheres(),new Spheres(),new Spheres(),new Spheres(Keys.W),new Spheres()};
            IsAutoCastSkills = new bool[10] { false, false, true, true, true, true, false, false, false, true, };
            Threshold = 24;
        }

        public static Skills CreatFromXmlFile(string path)
        {
            Skills myskills;
            using (FileStream fs = new FileStream(path, FileMode.Open))
             {
                 XmlSerializer formatter = new XmlSerializer(typeof(Skills));
                 myskills = (Skills)formatter.Deserialize(fs);
             }
            return myskills;
        }

        public void SaveData()
        {
            this.SaveData("Keys.xml");
        }

        public void SaveData(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Skills));
                formatter.Serialize(fs, this);
            }
        }
    }
}
