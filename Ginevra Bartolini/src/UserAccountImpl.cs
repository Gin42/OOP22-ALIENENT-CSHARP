public class UserAccountImpl : UserAccountImpl
{   
    private string _nickname;
    private int _money;
    private int _highscore;
    private readonly Dictionary<string,int> _inventory;
    private readonly Dictionary<Statistic,int> _toAddPwu
    public UserAccountImpl(Dictionary<string,int> inventory, Dictionary<Statistic,int> toAddPwu)
    {
        _inventory = inventory;
        _toAddPwu = toAddPwu;
    }

    int Money
    {
        get => _money;
        set => _money =  value;
    }

    string Nickname
    {
        get => _nickname;
        set => _nickname =  value;
    }

    int Highscore
    {
        get => _highscore;
        set => _highscore =  value;
    }

    Dictionary<string,int> Inventory => _inventory;
    Dictionary<Statistic,int> ToAddPwu => _toAddPwu;
    
    int GetCurrLevel(string id)
    {
       return _inventory.Item(id);
    }
    
    void UpdateInventory(string id)
    {
         _inventory[id] = _inventory.TryGetValue(id, out int existingvalue) ? existingvalue+1 :1;    
    }
   
    void UpdateToAddPwu(Dictionary<Statistic, int> mapToAdd)
    {
        if ( _toAddPwu.Count() == 0 )
        {
            _toAddPwu = new Dictionary<Statistic, int> mapToAdd;
        } else
        {
            foreach(KeyValuePair<Statistic, int> entry in mapToAdd)
            {
                _toAddPwu[entry.Key] = _toAddPwu.contains(entry.Key) ? _toAddPwu[entry.Key] + entry.Value : entry.Value;
            }
        }
 
    }
}