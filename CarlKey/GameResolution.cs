using System;
using Microsoft.Win32;

namespace CarlKey
{
    class GameResolution
    {
        public readonly int CDPointX;
        public readonly int CDPointY;

        private static double CDWidthProportional ;
        private static double CDHeightPrportional ;

        public readonly int Width;
        public readonly int Height;

        public readonly int SecondSkillPointX;
        public readonly int SecondSkillPointY;
        

        public GameResolution()
        {
            try
            {
                string widthValue = string.Empty;
                string heightValue = string.Empty;

                RegeditLoader.GetRegVal(Registry.CurrentUser,
                @"Software\Blizzard Entertainment\Warcraft III\Video", "reswidth", out widthValue);

                RegeditLoader.GetRegVal(Registry.CurrentUser,
                @"Software\Blizzard Entertainment\Warcraft III\Video", "resheight", out heightValue);

                double width = (int)double.Parse(widthValue);
                double height = (int)double.Parse(heightValue);

                double AspectRatio = Math.Round(width / height);

                this.Width = (int)width;
                this.Height = (int)height;

                if (AspectRatio >= 1.6)
                {
                    CDWidthProportional = 0.9826388888888889;
                    CDHeightPrportional = 0.9866666666666667;
                }
                else
                {
                    CDWidthProportional = 0.982421875;
                    CDHeightPrportional = 0.9869791666666667;
                }
                CDPointX = (int)Math.Round(Width * CDWidthProportional);
                CDPointY = (int)Math.Round(Height * CDHeightPrportional);
                SecondSkillPointX = ((int)Math.Round(Width * 0.904296875));
                SecondSkillPointY = ((int)Math.Round(Height * 0.8828125));
            }
            catch
            {
                throw new Exception("检测不到电脑安装了魔兽争霸");
            }
        }
    }
}
