using AlienentShop.Impl;

namespace AlienentShop.Api
{
    public interface IInitController
    {
        void Init(FakeController controller, IFakeScene? scene);
    }
}