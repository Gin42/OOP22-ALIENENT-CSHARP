using System.Numerics;
using AlienEnt.GameObject.Component.Api;
using AlienEnt.Geometry;

namespace AlienEnt.GameObject.Component
{
    /// <summary>
    /// A simple implementation of ShooterComponent.
    /// </summary>
    public class BasicShooterComponent : AbstractComponent, IShooterComponent
    {

        private int _delay;
        private double _counter = 0;

        private int _damage;
        private int _speed;
        private IHitboxComponent.GameObjectType _type;

        private bool _trigger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject">the referenced object</param>
        /// <param name="enabled">if the component must be enabled</param>
        /// <param name="shot">the supplier of the projectiles</param>
        public BasicShooterComponent(IGameObject gameObject, bool enabled, Func<IGameObject> shot) : base(gameObject, enabled)
        {
            ProjectileSupplier = shot;
        }

        /// <inheritdoc/>
        public Func<IGameObject> ProjectileSupplier { get; set; }

        /// <inheritdoc/>
        public void Shoot()
        {
            _trigger = true;
        }

        /// <inheritdoc/>
        public override void Start()
        {
            var obj = GetGameObject();
            _delay = obj.GetStatValue(Statistic.COOlDOWN) ?? 0;
            _damage = obj.GetStatValue(Statistic.DAMAGE) ?? 0;
            _speed = obj.GetStatValue(Statistic.PROJECTILESPEED) ?? 0;
            _type = obj.GetComponent<IHitboxComponent>()?.ObjectType ?? 
                    throw new NullReferenceException("The object doesn't posses an hitbox");
        }
        
        /// <inheritdoc/>
        public override void Update(double deltaTime)
        {
            if(!IsEnabled())
                return;
            if(_counter < _delay)
                _counter += deltaTime;
            if(_trigger && _counter >= _delay)
            {
                var p = ProjectileSupplier();
                p.SetStatValue(Statistic.DAMAGE, _damage);
                p.SetStatValue(Statistic.SPEED, _speed);
                var hb = p.GetComponent<IProjectileHitboxComponent>();
                if(hb != null)
                    hb.ShooterType = _type;
                p.Velocity = Vector2D.FromAngleAndModule(GetGameObject().Velocity.Angle, _speed);
                p.Position = GetGameObject().Position;
                foreach (var comp in p.GetAllComponents())
                    comp.Start();
                _counter = 0;
            }
            _trigger = false;
        }

        /// <inheritdoc/>
        public override IComponent? Duplicate(IGameObject obj) => new BasicShooterComponent(obj, false, ProjectileSupplier);
    }
}