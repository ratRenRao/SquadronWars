namespace Assets.GameClasses
{
    public class Equipment : Item
    {
        public Stats stats;

        public Equipment(string name, ItemType itemType, Stats stats)
        {
            this.stats = stats;
            this.name = name;
            this.itemType = itemType;
        }

    }


}
