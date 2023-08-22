using System.Reflection;
using System.Runtime.ConstrainedExecution;
using AlienEnt.Geometry;

namespace AlienEnt.GameObject.Component
{
    public class PlayerInputComponent : AbstractComponent, IPlayerInputComponent
    {
        private const double ANG_VEL = 360;
        private const double ACC_RATE = 3;

        private IShooterComponent? _shooter;

        private int _maxSpeed;
        private double _acc;

        public PlayerInputComponent(IGameObject gameObject, IInputSupplier input) : base(gameObject, true)
        {
            InputSupplier = input;
        }

        public IInputSupplier InputSupplier { get; set; }

        public IShooterComponent? ShooterComponent => _shooter;

        public override void Start()
        {
            _maxSpeed = GetGameObject().GetStatValue(Statistic.SPEED) ?? 0;
            _acc = _maxSpeed * ACC_RATE;
            _shooter = GetGameObject().GetComponent<IShooterComponent>();
        }

        public override void Update(double deltaTime)
        {
            Vector2D vel = GetGameObject().Velocity;
            foreach(var input in InputSupplier.GetInputs())
            {
                double module = vel.Module;
                switch (input)
                {
                    case IInputSupplier.Input.ACCELERATE:
                        if (module < _maxSpeed)
                        {
                            double accTime = _acc * deltaTime;
                            if(module + accTime > _maxSpeed)
                                vel = Vector2D.FromAngleAndModule(vel.Angle, _maxSpeed);
                            else
                                vel = Vector2D.FromAngleAndModule(vel.Angle, module + accTime);
                        }
                        break;
                    case IInputSupplier.Input.STOP_ACCELERATE:
                        if(module > 0)
                        {
                            double accTime = _acc * deltaTime;
                            if (module - accTime < 0)
                                vel = Vector2D.FromAngleAndModule(vel.Angle, 0);
                            else
                                vel = Vector2D.FromAngleAndModule(vel.Angle, module - accTime);
                        }
                        break;
                    case IInputSupplier.Input.TURN_LEFT:
                        vel = Vector2D.FromAngleAndModule(vel.Angle - (ANG_VEL * deltaTime), module);
                        break;
                    case IInputSupplier.Input.TURN_RIGHT:
                        vel = Vector2D.FromAngleAndModule(vel.Angle + (ANG_VEL * deltaTime), module);
                        break;
                    case IInputSupplier.Input.SHOOT:
                        _shooter?.Shoot();
                        break;
                    default:
                        break;
                }
            }
        }

        public override IComponent? Duplicate(IGameObject obj)
        {
            Type type = InputSupplier.GetType();
            ConstructorInfo? constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
                return null;
            return new PlayerInputComponent(obj, (IInputSupplier) constructor.Invoke(null));
        }
    }
}