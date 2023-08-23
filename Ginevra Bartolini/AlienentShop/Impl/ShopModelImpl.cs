using AlienentShop.Api;

namespace AlienentShop.Impl
{
    public class ShopModelImpl : IShopModel
    {   
        private readonly FakeController _controller;
        private List<IPowerUp> _powerUps = new();

        public ShopModelImpl(FakeController controller)
        {
            _controller =  controller;
        }

        public void LoadPwu(List<IPowerUp> pwu)
        {
            foreach (var p in pwu)
            {
                _powerUps.Add(p);
            }
        }
        public int? Check(string id)
        {
            IUserAccount? user = _controller.Account;
            return _powerUps.AsEnumerable().Where(p => p.Id.Equals(id))
                    .Where(p => (user?.Money
                            - p.Cost * (_controller.Account?.GetCurrLevel(id) + 1)) >= 0)
                    .Select(p => -p.Cost * (_controller.Account?.GetCurrLevel(id) + 1)).FirstOrDefault();
        }
        public void UpdateShop(string id, int changeMoney)
        {
            IUserAccount user = _controller.Account;
            user.UpdateInventory(id);
            user.Money = changeMoney;
            UpdateToAddPwu(id, user);
        }

        private IUserAccount UpdateToAddPwu(string id, IUserAccount account) {
            
            var mapToAdd = _powerUps.AsEnumerable().Where(p => p.Id.Equals(id, StringComparison.Ordinal)).Select(p => p.StatModifiers).FirstOrDefault();
            if ( mapToAdd != null )
            {   
                account.UpdateToAddPwu(mapToAdd);
            }
            return account;
        }
    }
}