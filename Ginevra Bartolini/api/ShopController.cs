public interface ShopController
{
    UserAccount Account { get; }

    List<PowerUp> Pwu { get; }

    void loadPwuYaml();

    bool buy(string id);

}