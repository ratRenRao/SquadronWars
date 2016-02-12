using System;

namespace Assets.GameClasses
{
    public class Ability : IEffectable
    {
        public int AbilityId { get; set; }
        public int CharacterId { get; set; }
        public int AbilityLevel { get; set; }
        public void execute(ref Stats charStats)
        {
            throw new NotImplementedException();
        }

        public void immediateEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }

        public void lingeringEffect(ref Stats charStats)
        {
            throw new NotImplementedException();
        }

        public void removeEffect(ref Stats charStat)
        {
            throw new NotImplementedException();
        }
    }
}