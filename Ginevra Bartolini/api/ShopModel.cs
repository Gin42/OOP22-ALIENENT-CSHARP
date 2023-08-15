public interface ShopModel
{
    void loadPwu(List<PowerUp> pwu);
    Nullable<int> check(String id);
    void updateShop(string id, int changeMoney);
}
