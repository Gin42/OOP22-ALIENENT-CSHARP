using System.ComponentModel;

namespace Commons.Bounds
{
    public class Dimensions : IDimensions
    {
        private static readonly double s_screenWidthPercent = 1.0;
        private static readonly double s_screenHeightPercent = 0.8;

        public Dimensions (double width, double height) 
        {
            Height = height;
            Width = width;
        }
        public Dimensions () 
        {
            Width *= s_screenWidthPercent;
            Height *= s_screenHeightPercent;
        }

        [DefaultValue(600.0)]
        public double Width { private set; get;}
        [DefaultValue(600.0)]
        public double Height { private set; get;}
    }
}