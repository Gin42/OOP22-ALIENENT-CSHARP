namespace AlienEnt.GameObject.Component
{
    public class PlayerPowerUpComponent : AbstractComponent, IPowerUpComponent
    {
        public PlayerPowerUpComponent(GameObject gameObject) : base(gameObject, false)
        {

        }

        public void SetPowerUps(Dictionary<Statistic, int> powerUps)
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
                GetGameObject().Hit(newHp);
        }

        public override IComponent? Duplicate(IGameObject obj)
        {
            throw new NotImplementedException();
        }
    }
}