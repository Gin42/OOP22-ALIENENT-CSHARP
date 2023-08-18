using YamlDotNet

public class ShopControllerImpl : ShopController, InitController
{
    private static readonly string YML = ".yml";
    private static readonly string PWU = "/PowerUps";
    private static readonly string PWUREN = "/PowerUpsInfo";
    private static readonly string DIRYML = "/yaml";
    //private static readonly Logger LOGGER = LoggerFactory.getLogger(UserAccountHandlerImpl.class);

    private Controller _controller;
    private UserAccount _account;
    private ShopModel _model;
    private List<PowerUp> _powerUps = new List<PowerUp>();   

    public void init(Controller controller, FakeScene scene)
    {
        _controller = controller;
        _account = _controller.Account;
        _model = new ShopModelImpl(_controller);
        _model.loadPwu(_powerUps);
    } 
    public UserAccount Account { 
        get => _account; 
    }
    public List<PowerUp> Pwu { 
        get => _powerUps;
    }
    public void loadPwuYaml()
    {
         try (InputStream inputStream = getClass().getResourceAsStream(DIRYML + PWU + YML)) {

            final Constructor constructor = new Constructor(PowerUpImpl.class, new LoaderOptions());
            final TypeDescription accountDescription = new TypeDescription(PowerUpImpl.class);
            accountDescription.addPropertyParameters("id", String.class);
            accountDescription.addPropertyParameters("cost", Integer.class);
            accountDescription.addPropertyParameters("maxLevel", Integer.class);
            accountDescription.addPropertyParameters("statModifiers", Statistic.class, Integer.class);
            constructor.addTypeDescription(accountDescription);

            final Yaml yaml = new Yaml(constructor);
            final Iterable<Object> documents = yaml.loadAll(inputStream);

            for (final Object object : documents) {
                powerUps.add((PowerUp) object);
            }

        } catch (IOException e) {
            LOGGER.error("Could not open power ups file", e);
        }
    }

        public bool buy(string id)
    {
        Nullable<int> changeMoney = _model.check(id);
        if (changeMoney != null) {
            _model.updateShop(id, changeMoney.Value);
            return true;
        } else {
            return false;
        }

    }
}
