namespace AlienentShop.Api
{
    /*  The original java class implemented other methods.
        They were left out since useless for what I decided to test,
        which is the shop buying system.
        Omitted methods:
        getPwuInfo, loadPwuInfo, closeShop.
    */
    public interface IShopController
    {
        IUserAccount Account { get; }

        List<IPowerUp> Pwu { get; }

        void LoadPwuYaml();

        bool Buy(string id);

    }
}