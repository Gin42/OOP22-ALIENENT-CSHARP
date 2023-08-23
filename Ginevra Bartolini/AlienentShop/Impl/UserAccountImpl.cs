using AlienentShop.Api;

namespace AlienentShop.Impl
{
    /*  The class doesn't present an empty constructor, like its 
        java counterpart, since it was only used from the yaml 
        configuration file. Since that feature is not the focus of my
        test it was omitted. 
    */
    public class UserAccountImpl : IUserAccount
    {   
        private static readonly int LIMIT = 1000;

        private static readonly int MAX_MONEY = int.MaxValue - LIMIT;

        private string _nickname = "";

        private int _money = 0;

        private int _highscore = 0;

        private Dictionary<string,int> _inventory =  new();

        private Dictionary<IStatistic,int> _toAddPwu = new();

        public UserAccountImpl(string nickname) {
            _nickname = nickname;
            _money = 0;
            _highscore = 0;
        }

        public int Money
        {
            get => _money;
            set
            {
                if ((double) _money + value < MAX_MONEY)
                {
                    _money += value;
                }
            } 
        }

        public string Nickname
        {
            get => _nickname;
            set => _nickname = value;
        }

        public int Highscore
        {
            get => _highscore;
            set => _highscore = value;
        }

        public Dictionary<string, int> Inventory
        { 
            get => _inventory; 
            set => _inventory = new Dictionary<string, int>(value);
        }

        public Dictionary<IStatistic, int> ToAddPwu
        { 
            get => _toAddPwu;
            set => _toAddPwu =  new Dictionary<IStatistic, int>(value);
        }

        public int GetCurrLevel(string id)
        {
            return _inventory.ContainsKey(id) ? _inventory[id] : 0;
        }
        
        public void UpdateInventory(string id)
        {
            _inventory[id] = _inventory.ContainsKey(id) ? _inventory[id] + 1 : 1;    
        }
    
        public void UpdateToAddPwu(Dictionary<IStatistic, int> mapToAdd)
        {
            if (_toAddPwu.Count == 0)
            {
                _toAddPwu = new Dictionary<IStatistic, int>(mapToAdd);
            } else
            {
                foreach (KeyValuePair<IStatistic, int> entry in mapToAdd)
                {
                    _toAddPwu[entry.Key] = _toAddPwu.ContainsKey(entry.Key) ? _toAddPwu[entry.Key] + entry.Value : entry.Value;
                }
            }
    
        }
    }
}