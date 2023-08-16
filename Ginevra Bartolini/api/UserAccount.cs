public interface UserAccount
{
    int Money
    {
        get;
        set;
    }

    string Nickname
    {
        get;
        set;
    }

    int Highscore
    {
        get;
        set;
    }

    Dictionary<string,int> Inventory
    {
        get;
    }
    Dictionary<Statistic,int> ToAddPwu
    {
        get;
    }
    
    int GetCurrLevel(string id);
    
    void UpdateInventory(string id);
   
    void UpdateToAddPwu(Dictionary<Statistic, int> mapToAdd);
}