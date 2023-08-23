using AlienEnt.Commons;
using AlienEnt.GameWorld;

namespace AlienEnt.Props
{
    public class PropPlayerSpawner
    {
        private readonly static Dictionary<PropStatistic, int> s_defaultStats = new() {{PropStatistic.HP, 50}};
        private readonly IWorld _world;

        public PropPlayerSpawner(IWorld world)
        {
            _world = world;
        }

        public PropGameObject GetPlayer(string Id)
        {
            var player = new PropGameObject(Point2D.Origin, s_defaultStats, Id)
            {
                InputSupplier = new PropInputSupplier()
            };
            return player;
        }
    }
}