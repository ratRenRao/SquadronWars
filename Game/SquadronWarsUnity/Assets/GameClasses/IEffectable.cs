using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;

namespace Assets.GameClasses
{
    public interface IEffectable
    {
        void Initialize(ref List<Stats> affectedCharacters, ref Stats executionerStats);
        void Execute();
        void ImmediateEffect(Stats stats);
        void RemoveEffect(Stats stats);
        void LingeringEffect(Stats stats);
    }
}
