using System.ComponentModel;
using AlienEnt.Commons;
using AlienEnt.GameWorld;

namespace AlienEnt.Props
{
    public class PropEnemySpawner
    {
        private static readonly int s_maxEnemies = 10;
        private static readonly double s_timeBeforeSpawn = 5.0;
        private static readonly Random s_random = new();
        private readonly Point2D _bottomLeft;
        private readonly Point2D _topRight;
        private readonly IWorld _world;
        [DefaultValue(0.0)]
        private double _timeSinceSpawn;
        public PropEnemySpawner(Point2D topRight, Point2D bottomLeft, IWorld world, PropGameObject player)
        {
            _world = world;
            _bottomLeft = bottomLeft;
            _topRight = topRight;
        }

        public void Update(double deltaTime)
        {
            _timeSinceSpawn += deltaTime;
            if (_timeSinceSpawn >= s_timeBeforeSpawn)
            {
                SpawnEnemy();
                _timeSinceSpawn = 0;
            }
        }

        private void SpawnEnemy()
        {
            if(_world.EnemyCount < s_maxEnemies)
            {
                //NO DA FARE
                var X = s_random.NextDouble() * s_maxEnemies;
            }
        }
    }
}