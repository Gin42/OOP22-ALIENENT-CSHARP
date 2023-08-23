using YamlDotNet.Serialization;

namespace AlienentShop.Api
{
    public interface IPowerUp
    {
        string Id { get; set; }

        int Cost { get; set; }

        int MaxLevel { get; set; }
        
        Dictionary<IStatistic, int> StatModifiers { get; set; }
    }
}
