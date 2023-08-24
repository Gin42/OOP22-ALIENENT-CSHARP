using Alienent.Api;
using Alienent.geometry;

namespace Alienent
{
    public interface IBoundaryHitboxComponent : IHitboxComponent
    {
        enum Locations
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

        void SetLocations(Locations location);

        Line2D GetLine();
    }
}