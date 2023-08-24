using System.ComponentModel;
using AlienEnt.CollisionHandler;
using AlienEnt.Commons;
using AlienEnt.Commons.Bounds;
using AlienEnt.Commons.Buffer;
using AlienEnt.Props;
using AlienEnt.Walls;

namespace AlienEnt.GameWorld 
{
    public sealed class World : IWorld
    {
        private readonly static IWallBuilder s_wallBuilder = new WallBuilder();
        private readonly static string[] s_givesScore = {"enemy"};
        private readonly ICollisionHandler _collisionHandler;
        private readonly IDoubleBuffer<PropGameObject> _doubleBuffer;
        private readonly ISet<PropGameObject> _lastAdded;
        private PropGameObject? _player;

        public World (IDimensions dimensions) {
            Dimensions = dimensions;
            _lastAdded = new HashSet<PropGameObject>();
            _doubleBuffer = new DoubleBuffer<PropGameObject>();
            _collisionHandler = new SimpleCollisionHandler();
            CreateWalls();
        }

        public IDimensions Dimensions { get; private set;}
        public ISet<PropGameObject> LastAdded 
        { 
            get 
            {
                var ret = new HashSet<PropGameObject>(_lastAdded);
                _lastAdded.Clear();
                return ret;
            }
        }
        public PropGameObject? Player 
        { 
            get => _player;
            set
            {
                if (value != null)
                {
                    _player = value;
                    AddGameObject(value);
                }
            }
        }
        public bool? IsOver 
        { 
            get
            {
                return !_player?.IsAlive;
            } 
        }
        [DefaultValue(0)]
        public int Score { private set; get; }
        [DefaultValue(0)]
        public int EnemyCount { private set; get; }

        public void AddAllGameObjects(params PropGameObject[] obj)
        {
            foreach (var o in obj)
            {
                AddGameObject(o);
            }
        }

        public void AddGameObject(PropGameObject obj)
        {
            if (s_givesScore.Contains(obj.Tag)) {
                EnemyCount++;
            }
            _doubleBuffer.Buffer.Add(obj);
            _collisionHandler.AddHitbox(obj.Hitbox);
            _lastAdded.Add(obj);
        }

        public void Update(double deltaTime)
        {
            _doubleBuffer.ChangeBuffer();
            foreach (var obj in _doubleBuffer.Current)
            {
                if (obj.IsAlive) 
                {
                    obj.Update(deltaTime);
                }
                else
                {
                    RemoveGameObject(obj);
                }
            }
        }

        private void RemoveGameObject(PropGameObject obj) 
        {
            if (s_givesScore.Contains(obj.Tag)) {
                Score += obj.Hp * 100;
                EnemyCount--;
            }
            _doubleBuffer.Buffer.Remove(obj);
            _collisionHandler.RemoveHitbox(obj.Hitbox);
        }

        private void CreateWalls(){
            var walls = new List<PropGameObject>();

            // Create upper wall.
            s_wallBuilder.AddPresetGameObject();
            s_wallBuilder.AddBoundaryHitboxComponent(Point2D.Origin, new(Dimensions.Width, 0));
            s_wallBuilder.SetLocation(PropBoundaryHitbox.Locations.UP);
            walls.Add(s_wallBuilder.GetWall());
            s_wallBuilder.Clear();

            // Create lower wall.
            s_wallBuilder.AddPresetGameObject();
            s_wallBuilder.AddBoundaryHitboxComponent(new(0, Dimensions.Height), new(Dimensions.Width, Dimensions.Height));
            s_wallBuilder.SetLocation(PropBoundaryHitbox.Locations.DOWN);
            walls.Add(s_wallBuilder.GetWall());
            s_wallBuilder.Clear();

            // Create right wall.
            s_wallBuilder.AddPresetGameObject();
            s_wallBuilder.AddBoundaryHitboxComponent(new(Dimensions.Width, 0), new(Dimensions.Width, Dimensions.Height));
            s_wallBuilder.SetLocation(PropBoundaryHitbox.Locations.RIGHT);
            walls.Add(s_wallBuilder.GetWall());
            s_wallBuilder.Clear();

            //Create left wall.
            s_wallBuilder.AddPresetGameObject();
            s_wallBuilder.AddBoundaryHitboxComponent(Point2D.Origin, new(0, Dimensions.Height));
            s_wallBuilder.SetLocation(PropBoundaryHitbox.Locations.LEFT);
            walls.Add(s_wallBuilder.GetWall());
            s_wallBuilder.Clear();


            walls.ForEach(w => {
                _doubleBuffer.Buffer.Add(w);
                _collisionHandler.AddHitbox(w.Hitbox);
            });
        }
    }
}