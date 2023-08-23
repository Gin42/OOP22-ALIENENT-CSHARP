using AlienentShop.Api;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AlienentShop.Impl
{
    /*  To understand the reason behind omitted methods and fields,
        see AlienentShop.Api IShopController.
    */
    public class ShopControllerImpl : IShopController, IInitController
    {
        private static readonly string YML = ".yml";
        private static readonly string PWU = "PowerUps";

        private FakeController _controller = new();
        private IUserAccount _account = new UserAccountImpl("");
        private IShopModel _model = new ShopModelImpl(new FakeController());
        private readonly List<IPowerUp> _powerUps = new();   

        public void Init(FakeController controller, IFakeScene? scene)
        {
            _controller = controller;
            _account = _controller.Account;
            _model = new ShopModelImpl(_controller);
            _model.LoadPwu(_powerUps);
        } 

        public IUserAccount Account => _account; 

        public List<IPowerUp> Pwu  => _powerUps;

        public void LoadPwuYaml()
        {              
            string filename = PWU + YML;
            if (File.Exists(filename)) 
            {
                string content =  File.ReadAllText(filename);
                string[] documents = content.Split(new[] {"---"}, 
                        StringSplitOptions.RemoveEmptyEntries);

                var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

                foreach (var doc in documents)
                {
                    try
                    {
                        PowerUpImpl custom = deserializer.Deserialize<PowerUpImpl>(doc);
                        _powerUps.Add(custom);
                    } catch (YamlException)
                    {

                    } 
                }
            }
        }

        public bool Buy(string id)
        {
            int? changeMoney = _model.Check(id);
            if (changeMoney.HasValue) 
            {
                _model.UpdateShop(id, changeMoney.Value);
                return true;
            } else 
            {
                return false;
            }

        }
    }
}
