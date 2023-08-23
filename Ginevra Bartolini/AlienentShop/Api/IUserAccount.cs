namespace AlienentShop.Api
{
    public interface IUserAccount
    {
        int Money { get; set; }

        string Nickname { get; set; }

        int Highscore { get; set; }

        Dictionary<string,int> Inventory { get; set; }

        Dictionary<IStatistic,int> ToAddPwu { get; set; }
        
        int GetCurrLevel(string id);
        
        void UpdateInventory(string id);
    
        void UpdateToAddPwu(Dictionary<IStatistic, int> mapToAdd);
    }
}
