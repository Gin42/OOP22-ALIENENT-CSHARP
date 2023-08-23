namespace AlienentShop.Api
{
    public interface IShopController
    {
        IUserAccount Account { get; }

        List<IPowerUp> Pwu { get; }

        void LoadPwuYaml();

        bool Buy(string id);

    }
}