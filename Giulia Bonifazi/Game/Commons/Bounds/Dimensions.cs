using System.ComponentModel;

namespace Commons.Bounds
{
    public class Dimensions : IDimensions
    {
        private static readonly double s_ScreenWidthPercent = 1.0;
        private static readonly double s_ScreenHeightPercent = 0.8;

        [DefaultValue(600.0)]
        public double Width { private set; get;}
        [DefaultValue(600.0)]
        public double Height { private set; get;}

        public Dimensions (double width, double height) {
            Height = height;
            Width = width;
        }
        public Dimensions () {
            Width *= s_ScreenWidthPercent;
            Height *= s_ScreenHeightPercent;
        }
    }
}