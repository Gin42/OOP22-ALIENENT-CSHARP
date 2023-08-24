using System.ComponentModel;

namespace AlienEnt.Commons.Bounds
{
    /// <summary>
    /// This is a more basic implementation of Dimensions than its java counterpart, as 
    /// it was not relevant to the tests and it was only needed for other class implementations.
    /// </summary>
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