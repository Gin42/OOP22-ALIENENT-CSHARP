using AlienentShop.Api;

namespace AlienentShop.Impl
{
    public class FakeController
    {
        private IUserAccount _account = new UserAccountImpl("");

    public IUserAccount Account
    { 
        get => _account;
        set => _account = value;
    }

    }
}
