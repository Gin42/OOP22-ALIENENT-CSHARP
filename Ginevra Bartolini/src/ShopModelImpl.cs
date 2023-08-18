public class ShopModelImpl : ShopModel
{   
    private readonly Controller _controller;
    private List<PowerUp> _powerUps = new List<PowerUp>();

    public ShopModelImpl(Controller controller)
    {
        _controller =  controller;
    }

    public void loadPwu(List<PowerUp> pwu)
    {
        _powerUps = pwu;
    }
    public Nullable<int> check(string id)
    {
        UserAccount? user = _controller.Account;
        return _powerUps.AsEnumerable().Where(p => p.Id.Equals(id))
                .Where(p => (user?.Money
                        - p.Cost * (_controller.Account?.GetCurrLevel(id) + 1)) >= 0)
                .Select(p => -p.Cost * (_controller.Account?.GetCurrLevel(id) + 1)).FirstOrDefault();
    }
    public void updateShop(string id, int changeMoney)
    {
        UserAccount user = _controller.Account;
        user.UpdateInventory(id);
        user.Money = changeMoney;
        updateToAddPwu(id, user);
    }

    private UserAccount updateToAddPwu(string id, UserAccount account) {

        account.UpdateToAddPwu(
                _powerUps.AsEnumerable().Where(p => p.Id.Equals(id, StringComparison.Ordinal)).Select(p => p.StatModifiers).FirstOrDefault());
        return account;
    }
}