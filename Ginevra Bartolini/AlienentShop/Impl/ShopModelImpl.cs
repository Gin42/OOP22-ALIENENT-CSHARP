using AlienentShop.Api;

namespace AlienentShop.Impl
{
    public class ShopModelImpl : IShopModel
    {   
        private readonly FakeController _controller;
        private readonly List<IPowerUp> _powerUps = new();

        public ShopModelImpl(FakeController controller)
        {
            _controller =  controller;
        }

        public void LoadPwu(List<IPowerUp> pwu)
        {
            _powerUps.AddRange(pwu);
        }

        public int? Check(string id)
        {
            IUserAccount user = _controller.Account;
            return _powerUps.AsEnumerable().FirstOrDefault(p => p.Id.Equals(id) &&
                    (user.Money - p.Cost * (_controller.Account?.GetCurrLevel(id) + 1)) >= 0)
                    ?.Cost * -(_controller.Account?.GetCurrLevel(id) + 1) ?? null;
        }
        public void UpdateShop(string id, int changeMoney)
        {
            IUserAccount user = _controller.Account;
            user.UpdateInventory(id);
            user.Money = changeMoney;
            UpdateToAddPwu(id, user);
        }

        private IUserAccount UpdateToAddPwu(string id, IUserAccount account) {
            
            Dictionary<IStatistic, int>? mapToAdd = _powerUps.AsEnumerable().FirstOrDefault(p => p.Id.Equals(id))?.StatModifiers;
            if (mapToAdd != null)
            {   
                account.UpdateToAddPwu(mapToAdd);
            }
            return account;
        }
    }
}