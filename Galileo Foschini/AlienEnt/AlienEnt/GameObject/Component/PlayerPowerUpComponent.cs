using AlienEnt.GameObject.Component.Api;

namespace AlienEnt.GameObject.Component
{
    /// <summary>
    /// A class created to apply PowerUps to Players or other GameObjects.
    /// </summary>
    public class PlayerPowerUpComponent : AbstractComponent, IPowerUpComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public PlayerPowerUpComponent(IGameObject gameObject) : base(gameObject, false)
        {

        }
        
        /// <inheritdoc/>
        public void SetPowerUps(IDictionary<Statistic, int> powerUps)
        {
            double mod;
            int? oldStat;
            foreach(var s in powerUps.Keys)
            {
                mod = powerUps[s] / (double) 100 + 1.0;
                oldStat = GetGameObject().GetStatValue(s);
                if (oldStat.HasValue)
                    GetGameObject().SetStatValue(s, (int)(oldStat.Value * mod));
            }
            int newHp = (GetGameObject().GetStatValue(Statistic.HP) ?? 0) - GetGameObject().GetHealth();
            if(newHp > 0)
                GetGameObject().Heal(newHp);
            else
                GetGameObject().Hit(-newHp);
        }

        /// <inheritdoc/>
        public override IComponent? Duplicate(IGameObject obj)
        {
            var ret = new PlayerPowerUpComponent(obj);
            return ret;
        }
    }
}