using System.Collections.Generic;

namespace Assets.GameClasses
{
    public interface IEffectable
    {
        void Execute(List<Stats> affectedCharacters, ref Stats executionerStats);
        void ImmediateEffect();
        void RemoveEffect();
        void LingeringEffect();
    }
}
