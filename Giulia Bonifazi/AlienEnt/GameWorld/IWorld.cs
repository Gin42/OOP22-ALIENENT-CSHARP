using AlienEnt.Commons.Bounds;
using AlienEnt.Props;

namespace AlienEnt.GameWorld {
    /// <summary>
    /// World interface.
    /// </summary>

    public interface IWorld {
        
        IDimensions Dimensions { get; }
        ISet<PropGameObject> LastAdded { get; }
        PropGameObject? Player { get; set; }
        bool? IsOver { get; }
        int Score { get; }
        int EnemyCount { get; }

        void Update (double deltaTime);

        void AddGameObject(PropGameObject obj);

        void AddAllGameObjects(params PropGameObject[] obj);
        
    }
}