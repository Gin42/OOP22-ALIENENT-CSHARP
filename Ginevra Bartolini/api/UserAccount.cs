public interface UserAccount
{
    int Money
    {
        get;
        set;
    }

    string nickname
    {
        get;
        set;
    }

    int highscore
    {
        get;
        set;
    }

    Dictionary<string,int> inventory
    {
        get;
    }
    Dictionary<Statistic,int> ToAddPwu
    {
        get;
    }
    
    int getCurrLevel(String id);
    
    void updateInventory(String id);
   
    void updateToAddPwu(Dictionary<Statistic, int> mapToAdd);
}