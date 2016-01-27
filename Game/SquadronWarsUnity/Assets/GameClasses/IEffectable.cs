namespace Assets.GameClasses
{
    interface IEffectable
    {
        void execute(Stats charStats);
        void immediateEffect(ref Stats charStat);
        void removeEffect(ref Stats charStat);
        void lingeringEffect(ref Stats charStats);
    }
}
