using AlienentShop.Api;

namespace AlienentShop.Impl
{
    public class PowerUpImpl : IPowerUp
    {
        private string _id = "";
        private int _cost = 0;
        private int _maxLevel = 0;
        private Dictionary<IStatistic, int> _statModifiers = new();

        public PowerUpImpl()
        {
            
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

        public int MaxLevel
        {
            get => _maxLevel;
            set => _maxLevel = value;
        }

        public Dictionary<IStatistic, int> StatModifiers
        {
            get => _statModifiers;
            set => _statModifiers = value;
        }

    }
}