using System.Reflection;
using System.Runtime.ConstrainedExecution;
using AlienEnt.Geometry;

namespace AlienEnt.GameObject.Component
{
    /// <summary>
    /// PlayerInputComponent
    /// Handle the movements of the player.
    /// </summary>
    public class PlayerInputComponent : AbstractComponent, IPlayerInputComponent
    {
        private const double ANG_VEL = 360;
        private const double ACC_RATE = 3;

        private IShooterComponent? _shooter;

        private int _maxSpeed;
        private double _acc;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="input"></param>
        public PlayerInputComponent(IGameObject gameObject, IInputSupplier input) : base(gameObject, true)
        {
            InputSupplier = input;
        }

        /// <inheritdoc/>
        public IInputSupplier InputSupplier { get; set; }
        /// <inheritdoc/>
        public IShooterComponent? ShooterComponent => _shooter;
        
        /// <inheritdoc/>
        public override void Start()
        {
            _maxSpeed = GetGameObject().GetStatValue(Statistic.SPEED) ?? 0;
            _acc = _maxSpeed * ACC_RATE;
            _shooter = GetGameObject().GetComponent<IShooterComponent>();
        }

        /// <inheritdoc/>
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
            InputSupplier.ClearInputs();

            GetGameObject().Position = vel.Mul(deltaTime).Translate(GetGameObject().Position);
            GetGameObject().Velocity = vel;
        }

        /// <inheritdoc/>
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