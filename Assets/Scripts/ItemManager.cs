using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
    public static ItemManager manager;
    public Inventory playerInventory;
    public PaperDoll playerDoll;
    public GoldManager playerCurrency;

    void Awake()
    {
        manager = this;
    }

    public bool SwapItems(ItemHolder holderA, int slotA, ItemHolder holderB, int slotB)
    {
        Item itemA = holderA.GetItemInSlot(slotA);
        Item itemB = holderB.GetItemInSlot(slotB);
        Debug.Log(itemA.name + " " + itemB.name);
        if ((itemB.name == "" || holderA.CanPlace(itemB, slotA, holderB)) && (itemA.name == "" || holderB.CanPlace(itemA, slotB, holderA)))
        {
            holderA.PlaceItem(itemB, slotA, holderB);
            holderB.PlaceItem(itemA, slotB, holderA);
            return true;
        }
        return false;
    }
}
