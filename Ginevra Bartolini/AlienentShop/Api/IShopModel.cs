namespace AlienentShop.Api
{
    public interface IShopModel
    {
        void LoadPwu(List<IPowerUp> pwu);

        int? Check(string id);
        
        void UpdateShop(string id, int changeMoney);
    }
}
