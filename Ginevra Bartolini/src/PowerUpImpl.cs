public class PowerUpImpl : PowerUp
{
    private string _id;
    private int _cost;
    private int _maxLevel;
    private readonly Dictionary<Statistic, int> _statModifiers;
    public PowerUpImpl(Dictionary<Statistic, int> stat)
    {
        _statModifiers = stat;
    }

    string Id
    {
        get => _id;
        set => _id = value;
    }

    int Cost
    {
        get => _cost;
        set => _cost = value;
    }

    int getMaxLevel
    {
        get => _maxLevel;
        set => _maxLevel = value;
    }

    Dictionary<Statistic, int> StatModifiers => _statModifiers;

}