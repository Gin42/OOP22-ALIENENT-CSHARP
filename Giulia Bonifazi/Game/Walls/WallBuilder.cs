using Commons;
using Props;

namespace Walls
{
    public class WallBuilder : IWallBuilder
    {
        private static readonly string s_defaultId = "Mexico_border";
        private static readonly PropType s_defaultType = PropType.BOUNDARY;
        private static readonly Dictionary<PropStatistic, int> s_defaultStatistics = new Dictionary<PropStatistic, int>() 
        {
            { PropStatistic.HP, 999999 }
        };
        private PropBoundaryHitbox? _hitbox;
        private PropGameObject _gameObject;
        public PropGameObject GameObject 
        {
            get => _gameObject;
        }

        public WallBuilder() {
            _gameObject = new PropGameObject(Point2D.Origin, s_defaultStatistics, s_defaultId);
        }

        public void AddBoundaryHitboxComponent(Point2D p1, Point2D p2)
        {
            _hitbox = new PropBoundaryHitbox(_gameObject, true, s_defaultType, p1, p2);
            _gameObject.Hitbox = _hitbox;
        }

        public void AddGameObject(Point2D? pos, Dictionary<PropStatistic, int>? stats, string? id)
        {
            _gameObject = new PropGameObject(pos ?? Point2D.Origin,
                                             stats ?? s_defaultStatistics,
                                             id ?? s_defaultId);   
        }

        public void AddPresetGameObject()
        {
            _gameObject = new PropGameObject(Point2D.Origin, s_defaultStatistics, s_defaultId);
        }

        public void Clear()
        {
            AddPresetGameObject();
            _hitbox = null;
        }

        public PropGameObject GetWall()
        {
            return _gameObject;
        }

        public void SetLocation(PropBoundaryHitbox.Locations location)
        {
            if (_hitbox != null) 
            {
                _hitbox.Location = location;
            }
        }

        public void SetPosition(Point2D pos)
        {
            _gameObject.Pos = pos;
        }
    }
}