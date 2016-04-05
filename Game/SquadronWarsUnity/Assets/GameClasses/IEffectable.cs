using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;

namespace Assets.GameClasses
{
    public interface IEffectable
    {
        void Initialize(ref List<Character> characters, ref Stats executionerStats);
        void Execute();
        void ImmediateEffect(Stats stats);
        void RemoveEffect(ref Stats stats);
        void LingeringEffect(ref Stats stats);
    }
}
