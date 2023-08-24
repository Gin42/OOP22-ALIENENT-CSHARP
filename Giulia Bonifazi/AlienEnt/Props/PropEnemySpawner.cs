using AlienEnt.Commons;
using AlienEnt.GameWorld;

namespace AlienEnt.Props
{
    public class PropEnemySpawner
    {
        private static readonly Dictionary<PropStatistic, int> s_stats = new(){
            {PropStatistic.HP, 10}
        };
        private static readonly int s_maxEnemy = 50;
        private readonly IWorld _world;
        private double _timeSinceSpawn;
        private double _objX = 0;
        public static readonly double s_enemySpawnTime = 0.5;

        public PropEnemySpawner(Point2D topRight, Point2D bottomLeft, IWorld world, PropGameObject player)
        {
            _timeSinceSpawn = 0.0;
            _world = world;
        }

        public void Update(double deltaTime)
        {
            _timeSinceSpawn += deltaTime;
            if (_timeSinceSpawn > s_enemySpawnTime && _world.EnemyCount < s_maxEnemy)
            {
                _world.AddGameObject(new PropGameObject(new Point2D(_objX++, 0), s_stats, "enemy"));
                _timeSinceSpawn = 0.0;
            }
        }
    }
}