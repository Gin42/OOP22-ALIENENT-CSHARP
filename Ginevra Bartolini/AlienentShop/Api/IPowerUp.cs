using YamlDotNet.Serialization;

namespace AlienentShop.Api
{
    public interface IPowerUp
    {
        [YamlMember(Alias = "id")]
        string Id { get; set; }

        [YamlMember(Alias = "cost")]
        int Cost { get; set; }

        [YamlMember(Alias = "maxLevel")]
        int MaxLevel { get; set; }
        
        [YamlMember(Alias = "statModifiers")]
        Dictionary<IStatistic, int> StatModifiers { get; set; }
    }
}
