public class ShopControllerImpl : ShopController, InitController
{
    private static readonly string YML = ".yml";
    private static readonly string PWU = "/PowerUps";
    private static readonly string PWUREN = "/PowerUpsInfo";
    private static readonly string DIRYML = "/yaml";
    //private static readonly Logger LOGGER = LoggerFactory.getLogger(UserAccountHandlerImpl.class);

    private Controller controller;
    private UserAccount account;
    private ShopModel model;
    private List<PowerUp> powerUps = new List<>();   

    public void init(Controller controller, Page page)
    {
        this.controller = controller;
        this.account = controller.getUserAccount();
        this.model = new ShopModelImpl(this.controller);
        this.model.loadPwu(powerUps);
    } 
    public UserAccount Account { 
        get => account; 
    }
    public List<PowerUp> Pwu { 
        get => throw new NotImplementedException(); 
    }

    public bool buy(string id)
    {
        throw new NotImplementedException();
    }

    public void loadPwuYaml()
    {
        throw new NotImplementedException();
    }
}
