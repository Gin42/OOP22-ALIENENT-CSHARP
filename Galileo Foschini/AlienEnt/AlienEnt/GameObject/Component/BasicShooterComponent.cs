using System.Numerics;
using AlienEnt.Geometry;

namespace AlienEnt.GameObject.Component
{
    public class BasicShooterComponent : AbstractComponent, IShooterComponent
    {

        private int _delay;
        private double _counter = 0;

        private int _damage;
        private int _speed;
        private IHitboxComponent.GameObjectType _type;

        private bool _trigger;

        public BasicShooterComponent(IGameObject gameObject, bool enabled, Func<IGameObject> shot) : base(gameObject, enabled)
        {
            ProjectileSupplier = shot;
        }
        public Func<IGameObject> ProjectileSupplier { get; set; }

        public void Shoot()
        {
            _trigger = true;
        }

        public override void Start()
        {
            var obj = GetGameObject();
            _delay = obj.GetStatValue(Statistic.COOlDOWN) ?? 0;
            _damage = obj.GetStatValue(Statistic.DAMAGE) ?? 0;
            _speed = obj.GetStatValue(Statistic.PROJECTILESPEED) ?? 0;
            _type = obj.GetComponent<IHitboxComponent>()?.ObjectType ?? 
                    throw new NullReferenceException("The object doesn't posses an hitbox");
        }

        public override void Update(double deltaTime)
        {
            if(!IsEnabled())
                return;
            if(_counter < _delay)
                _counter += _delay;
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

        public override IComponent? Duplicate(IGameObject obj) => new BasicShooterComponent(obj, false, ProjectileSupplier);
    }
}