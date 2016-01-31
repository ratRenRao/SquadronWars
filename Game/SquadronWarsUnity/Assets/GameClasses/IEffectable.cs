namespace Assets.GameClasses
{
    public interface IEffectable
    {
        void execute(ref Stats charStats);
        void immediateEffect(ref Stats charStat);
        void removeEffect(ref Stats charStat);
        void lingeringEffect(ref Stats charStats);
    }
}
