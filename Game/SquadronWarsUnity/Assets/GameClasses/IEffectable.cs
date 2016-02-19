namespace Assets.GameClasses
{
    public interface IEffectable
    {
        void Execute(ref Stats characterStats);
        void ImmediateEffect(ref Stats characterStats);
        void RemoveEffect(ref Stats characterStats);
        void LingeringEffect(ref Stats characterStats);
    }
}
