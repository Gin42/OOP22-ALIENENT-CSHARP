using AlienentShop.Api;
using AlienentShop.Impl;
using YamlDotNet.RepresentationModel;

namespace AlienentShopTest
{
    /*  The omitted methods and fields where not usefull 
        for the focus of the test, which is the shop buying 
        system.
    */
    [TestClass]
    public class AlienentShopTest
    {
        private static readonly string NICKNAME1 = "afidfhkhi";
        private static readonly string NICKNAME2 = "oijawe";
        private static readonly string NICKNAME3 = "pASASCO";
        private static readonly string NICKNAME4 = "asclco";
        private static readonly int MONEY = 2_000_000;
        private static readonly int REMAINING_MONEY = 1_840_000;
        private static readonly string HEALTH = "Health";
        private static readonly string DAMAGE = "Damage";
        private static readonly string SPEED = "Speed";
        private static readonly int HEALTH_COST = 30_000;
        private static readonly int SPEED_COST = 20_000;
        private static readonly int DAMAGE_COST = 50_000;
        private static readonly int HEALTH_MAXLEVEL = 5;
        private static readonly int SPEED_MAXLEVEL = 3;
        private static readonly int DAMAGE_MAXLEVEL = 2;
        private static readonly int STAT = 5;
        private IUserAccount _account =  new UserAccountImpl("");
        private static readonly FakeController _contr = new();
        private readonly IInitController _controller = new ShopControllerImpl();
        private IShopController _shopController =  new ShopControllerImpl();
        private readonly IShopModel _model = new ShopModelImpl(_contr);
        private readonly List<IPowerUp> _pwu = new();
        private readonly Dictionary<IStatistic, int> _stats = new();
        private readonly Dictionary<IStatistic, int> _statsToAdd = new();

         [TestInitialize()]
        public void Init()
        {
            _shopController = (IShopController) _controller;
        }


        [TestMethod]
        public void TestLoadPwu() 
        {
            _account = new UserAccountImpl(NICKNAME1)
            {
                Money = MONEY
            };
            _contr.Account = _account ;
            _shopController.LoadPwuYaml();
            BuildPwuList();

            List<IPowerUp> list = _shopController.Pwu.AsEnumerable()
                        .Where(sp => _pwu.AsEnumerable().Any(lp => lp.Id == sp.Id)).ToList();
            foreach (var sp in list)
            {   
                IPowerUp? currPwu = _pwu.AsEnumerable().FirstOrDefault(lp => lp.Id == sp.Id) ?? null;
                Assert.IsNotNull(currPwu);
                Assert.AreEqual(currPwu.Cost, sp.Cost);
                Assert.AreEqual(currPwu.MaxLevel, sp.MaxLevel);
                Assert.IsFalse(sp.StatModifiers.Except(currPwu.StatModifiers).Any());
                Assert.IsTrue(sp.StatModifiers.Count == currPwu.StatModifiers.Count);
            }
        }

        [TestMethod]
        public void TestCheck() 
        {
            _account = new UserAccountImpl(NICKNAME2)
            {
                Money = MONEY
            };
            _contr.Account = _account ;
            _shopController.LoadPwuYaml();
            _model.LoadPwu(_shopController.Pwu);

            Assert.AreEqual(-HEALTH_COST, _model.Check(HEALTH));
            Assert.AreEqual(-SPEED_COST, _model.Check(SPEED));
            Assert.AreEqual(-DAMAGE_COST, _model.Check(DAMAGE));

            _account.Money = -_account.Money;
            Assert.AreEqual(null, _model.Check(HEALTH));
            Assert.AreEqual(null, _model.Check(SPEED));
            Assert.AreEqual(null, _model.Check(DAMAGE));
        }

        [TestMethod]
        public void TestUpdateShop() 
        {
            _account = new UserAccountImpl(NICKNAME3)
            {
                Money = MONEY
            };
            _contr.Account = _account ;
            _shopController.LoadPwuYaml();
            _model.LoadPwu(_shopController.Pwu);

            int? checkedPrice = _model.Check(HEALTH);
            if (checkedPrice!=null)
            {
                _model.UpdateShop(HEALTH, (int)checkedPrice);
                _model.UpdateShop(HEALTH, (int)checkedPrice * 2);
            }
            checkedPrice = _model.Check(DAMAGE);
            if (checkedPrice!=null)
            {
                _model.UpdateShop(DAMAGE, (int)checkedPrice);
            }
            checkedPrice = _model.Check(SPEED);
            if (checkedPrice!=null)
            {
                _model.UpdateShop(SPEED, (int)checkedPrice);
            }

            Assert.AreEqual(2, _account.GetCurrLevel(HEALTH));
            Assert.AreEqual(1, _account.GetCurrLevel(DAMAGE));
            Assert.AreEqual(1, _account.GetCurrLevel(SPEED));
            Assert.AreEqual(REMAINING_MONEY, _account.Money);

            Dictionary<IStatistic, int> toAdd = BuildToAddPwu();
            Assert.IsFalse(toAdd.Except(_account.ToAddPwu).Any());
            Assert.IsTrue(toAdd.Count == _account.ToAddPwu.Count);
        }

        [TestMethod]
        public void TestBuy() 
        {
           _account = new UserAccountImpl(NICKNAME4)
            {
                Money = MONEY
            };
            _contr.Account = _account ;
            _shopController.LoadPwuYaml();
            _controller.Init(_contr, null);

            Assert.IsTrue(_shopController.Buy(HEALTH));
            Assert.IsTrue(_shopController.Buy(HEALTH));
            Assert.IsTrue(_shopController.Buy(SPEED));
            Assert.IsTrue(_shopController.Buy(DAMAGE));

            Assert.AreEqual(2, _account.GetCurrLevel(HEALTH));
            Assert.AreEqual(1, _account.GetCurrLevel(DAMAGE));
            Assert.AreEqual(1, _account.GetCurrLevel(SPEED));
            Assert.AreEqual(REMAINING_MONEY, _account.Money);

            BuildToAddPwu();
            Assert.IsFalse(_statsToAdd.Except(_account.ToAddPwu).Any());
            Assert.IsTrue(_statsToAdd.Count == _account.ToAddPwu.Count);

            _account.Money = -REMAINING_MONEY;
            Assert.IsFalse(_shopController.Buy(DAMAGE));
        }
            
        private void BuildPwuList() 
        {
            IPowerUp health = new PowerUpImpl()
            {
                Id = HEALTH,
                Cost = HEALTH_COST,
                MaxLevel = HEALTH_MAXLEVEL
            };
            _stats.Add(IStatistic.HP, STAT);
            _stats.Add(IStatistic.SPEED, 0);
            _stats.Add(IStatistic.DAMAGE, 0);
            _stats.Add(IStatistic.DEFENCE, 0);
            _stats.Add(IStatistic.PROJECTILESPEED, 0);
            _stats.Add(IStatistic.COOLDOWN, 0);
            _stats.Add(IStatistic.RECOVERY, 0);
            foreach (var mod in _stats)
            {
                health.StatModifiers.Add(mod.Key,mod.Value);
            }
            _pwu.Add(health);

            IPowerUp speed = new PowerUpImpl()
            {
                Id = SPEED,
                Cost = SPEED_COST,
                MaxLevel = SPEED_MAXLEVEL
            };
            _stats[IStatistic.HP] = 0;
            _stats[IStatistic.SPEED] = STAT;
            foreach (var mod in _stats)
            {
                speed.StatModifiers.Add(mod.Key,mod.Value);
            }
            _pwu.Add(speed);

            IPowerUp damage = new PowerUpImpl()
            {
                Id = DAMAGE,
                Cost = DAMAGE_COST,
                MaxLevel = DAMAGE_MAXLEVEL
            };
            _stats[IStatistic.SPEED] = 0;
            _stats[IStatistic.DAMAGE] = STAT;
           foreach (var mod in _stats)
            {
                damage.StatModifiers.Add(mod.Key,mod.Value);
            }
            _pwu.Add(damage);
        }

         private Dictionary<IStatistic, int> BuildToAddPwu() 
         {
                _statsToAdd.Add(IStatistic.HP, STAT * 2);
                _statsToAdd.Add(IStatistic.SPEED, STAT);
                _statsToAdd.Add(IStatistic.DAMAGE, STAT);
                _statsToAdd.Add(IStatistic.DEFENCE, 0);
                _statsToAdd.Add(IStatistic.PROJECTILESPEED, 0);
                _statsToAdd.Add(IStatistic.COOLDOWN, 0);
                _statsToAdd.Add(IStatistic.RECOVERY, 0);
                return _statsToAdd;
        }

    }
}