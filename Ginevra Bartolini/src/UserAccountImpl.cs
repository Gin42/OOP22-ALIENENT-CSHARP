public class UserAccountImpl : UserAccount
{   
    private string _nickname = "";
    private int _money = 0;
    private int _highscore = 0;
    private readonly Dictionary<string,int> _inventory;
    private Dictionary<Statistic,int> _toAddPwu;
    public UserAccountImpl(Dictionary<string,int> inventory, Dictionary<Statistic,int> toAddPwu)
    {
        _inventory = inventory;
        _toAddPwu = toAddPwu;
    }

    public int Money
    {
        get => _money;
        set => _money +=  value;
    }

    public string Nickname
    {
        get => _nickname;
        set => _nickname =  value;
    }

    public int Highscore
    {
        get => _highscore;
        set => _highscore =  value;
    }

    public Dictionary<string,int> Inventory => _inventory;
    public Dictionary<Statistic,int> ToAddPwu => _toAddPwu;
    
    public int GetCurrLevel(string id)
    {
       return _inventory[id];
    }
    
    public void UpdateInventory(string id)
    {
         _inventory[id] = _inventory.TryGetValue(id, out int existingvalue) ? existingvalue+1 :1;    
    }
   
    public void UpdateToAddPwu(Dictionary<Statistic, int> mapToAdd)
    {
        if ( _toAddPwu.Count() == 0 )
        {
            _toAddPwu = new Dictionary<Statistic, int>(mapToAdd);
        } else
        {
            foreach(KeyValuePair<Statistic, int> entry in mapToAdd)
            {
                _toAddPwu[entry.Key] = _toAddPwu.ContainsKey(entry.Key) ? _toAddPwu[entry.Key] + entry.Value : entry.Value;
            }
        }
 
    }
}