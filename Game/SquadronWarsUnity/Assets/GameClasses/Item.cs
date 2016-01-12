namespace SquadronWars2
{
    public enum ItemType
    {
        Helm,
        Chest,
        Gloves,
        Legs,
        Shoulders,
        Boots,
        Accessory1,
        Accessory2,
    };

    public class Item : IWearable
    {

        public ItemType ItemType { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int ItemListId { get; set; }
    }
}