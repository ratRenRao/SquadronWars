using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Item : IWearable
{
    enum ItemType
    {
        Helm,
        Chest,
        Gloves,
        Legs,
        Shoulders,
        Boots,
        Accessory1,
        Accessory2
    };

    private ItemType itemType { get; set; }
    private string name { get; set; }
    private int id { get; set; }
    private int itemListId { get; set; }

}
