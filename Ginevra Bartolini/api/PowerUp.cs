public interface PowerUp
{
    string Id
    {
        get;
        set;
    }

    int Cost
    {
        get;
        set;
    }

    int getMaxLevel
    {
        get;
        set;
    }

    Dictionary<Statistic, int> StatModifiers
    {
        get;
    }

   
}
