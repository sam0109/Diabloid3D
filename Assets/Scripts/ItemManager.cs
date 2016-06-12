using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
    public static ItemManager manager;
    public Inventory myInventory;
    public PaperDoll myDoll;
    public GoldManager myCurrency;

    void Awake()
    {
        manager = this;
    }

    public bool SwapItems(ItemHolder holderA, int slotA, ItemHolder holderB, int slotB)
    {
        Item itemA = holderA.GetItemInSlot(slotA);
        Item itemB = holderB.GetItemInSlot(slotB);
        if ((itemB.name == "" || holderA.CanPlace(itemB, slotA, holderB)) && (itemB.name == "" || holderB.CanPlace(itemA, slotB, holderA)))
        {
            holderA.PlaceItem(itemB, slotA, holderB);
            holderB.PlaceItem(itemA, slotB, holderA);
            return true;
        }
        return false;
    }
}
