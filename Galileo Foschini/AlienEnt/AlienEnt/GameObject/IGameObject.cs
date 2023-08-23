using System.IO.Compression;
using AlienEnt.Geometry;
using AlienEnt.GameObject.Component.Api;

namespace AlienEnt.GameObject
{
    /// <summary>
    /// Models every object in the game map.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// The Position of the GameObject on the map
        /// </summary>
        Point2D Position{ get; set; }
        /// <summary>
        /// The actual Velocity of the GameObject
        /// </summary>
        Vector2D Velocity{ get; set; }

        /// <summary>
        /// The identifier of the object
        /// </summary>
        string Id{ get;}

        /// <summary>
        /// Return the input stat value.
        /// </summary>
        /// <param name="stat">the statistic whose value you want.</param>
        /// <returns>int of the input stat value</returns>
        int? GetStatValue(Statistic stat);
        
        /// <summary>
        /// return if the object is alive.
        /// </summary>
        /// <returns>true if the object is alive, false otherwise</returns>
        bool IsAlive();

        /// <summary>
        /// return the component of the given type or an empty optional if not present.
        /// </summary>
        /// <typeparam name="T">type of component you want</typeparam>
        /// <returns>The component of the given type</returns>
        T? GetComponent<T>() where T : IComponent
        {
            return (T?) GetAllComponents()
                    .FirstOrDefault(com => com is T, null);
        }

        /// <summary>
        /// return a list of all components.
        /// </summary>
        /// <returns>a list with all the objects component</returns>
        ISet<IComponent> GetAllComponents();

        /// <summary>
        /// return the stats dictionary.
        /// </summary>
        /// <returns>a dictionary with value of all stats</returns>
        IDictionary<Statistic,int> GetAllStats();

        /// <summary>
        /// Set the value of the given stat.
        /// </summary>
        /// <param name="stat">the statistic you want to set.</param>
        /// <param name="value">the value you want to assign.</param>
        void SetStatValue(Statistic stat, int value);

        /// <summary>
        /// Insert a new component.
        /// </summary>
        /// <param name="component">the new component you want to add.</param>
        void AddComponent(IComponent component);

        /// <summary>
        /// Insert multiple components at the game object.
        /// </summary>
        /// <param name="components">components to add to the object.</param>
        void AddAllComponents(ICollection<IComponent> components);

        /// <summary>
        /// subtract the damage from the life of the object.
        /// </summary>
        /// <param name="damage">amount of life to be subtracted.</param>
        void Hit(int damage);

        /// <summary>
        /// adds life to the object.
        /// </summary>
        /// <param name="heal">Amount of life to add.</param>
        void Heal(int heal);

        /// <summary>
        /// Return the health of the object.
        /// </summary>
        /// <returns>value of health.</returns>
        int GetHealth();

        /// <summary>
        /// heal the game object every second of the recovery statistic.
        /// </summary>
        /// <param name="deltatime">Time passed since the last cycle.</param>
        void Recovery(double deltatime);

        /// <summary>
        /// Update the game object.
        /// </summary>
        /// <param name="deltaTime">Time passed since the last cycle.</param>
        void Update(double deltaTime);
    }
}