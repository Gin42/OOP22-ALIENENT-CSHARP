using Commons;
using Props;

namespace Walls 
{
    public interface IWallBuilder 
    {
        PropGameObject GameObject { get; }

        void AddPresetGameObject();

        void AddGameObject(Point2D? pos, Dictionary<PropStatistic, int>? stats, string? id);

        void AddBoundaryHitboxComponent(Point2D p1, Point2D p2);
    
        void SetPosition(Point2D pos);

        void SetLocation(PropBoundaryHitbox.Locations location);

        PropGameObject GetWall();

        void Clear();
    }
}