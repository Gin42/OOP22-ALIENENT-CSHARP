using System.ComponentModel;
using System.Windows;

namespace Bounds {
    public class ArenaDimensions : Dimensions
    {
        private static double s_ScreenWidthPercent = 1.0;
        private static double s_ScreenHeightPercent = 0.8;

        [DefaultValue(600.0)]
        public double Width { private set; get;}
        [DefaultValue(600.0)]
        public double Height { private set; get;}

        public ArenaDimensions (double width, double height) {
            Height = height;
            Width = width;
        }
        public ArenaDimensions () {
            Width = Width * s_ScreenWidthPercent;
            Height = Height * s_ScreenHeightPercent;
        }
    }
}