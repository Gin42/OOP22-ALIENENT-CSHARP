using System.Drawing;
using Commons;

namespace Props {
    public class PropBoundaryHitbox : PropHitbox
    {

        public Locations Location{get; set;}    

        public enum Locations
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public PropBoundaryHitbox(PropGameObject obj, bool enabled, PropType objectType, 
            Point2D p1, Point2D p2) 
                : base(obj, Point2D.Origin)
            {
                
            }

    }
}