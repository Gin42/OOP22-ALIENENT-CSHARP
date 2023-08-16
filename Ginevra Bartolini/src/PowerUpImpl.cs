public class PowerUpImpl : PowerUp
{
    private string _id = "";
    private int _cost = 0;
    private int _maxLevel = 0;
    private readonly Dictionary<Statistic, int> _statModifiers;
    public PowerUpImpl(Dictionary<Statistic, int> stat)
    {
        _statModifiers = stat;
    }

    public string Id
    {
        get => _id;
        set => _id = value;
    }

    public int Cost
    {
        get => _cost;
        set => _cost = value;
    }

    public int getMaxLevel
    {
        get => _maxLevel;
        set => _maxLevel = value;
    }

    public Dictionary<Statistic, int> StatModifiers => _statModifiers;

}