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
        if (holderA.CanPlace(itemB, slotA) && holderB.CanPlace(itemA, slotB))
        {
            holderA.PlaceItem(itemB, slotA);
            holderB.PlaceItem(itemA, slotB);
            return true;
        }
        return false;
    }

    /*public bool SwapItems(int slotA, Slot.SlotOwner dollSlotA, int slotB, Slot.SlotOwner dollSlotB)
{
    if ((slotA >= 0 && ((dollSlotA == Slot.SlotOwner.PaperDoll && slotA < paperDollSize) || (dollSlotA != Slot.SlotOwner.PaperDoll && slotA < inventorySize))) &&
        (slotB >= 0 && ((dollSlotB == Slot.SlotOwner.PaperDoll && slotB < paperDollSize) || (dollSlotB != Slot.SlotOwner.PaperDoll && slotB < inventorySize))))
    {
        if (dollSlotA == Slot.SlotOwner.PaperDoll && dollSlotB == Slot.SlotOwner.PaperDoll)
        {
            print("Both doll");
            return false;
        }
        else if (dollSlotA == Slot.SlotOwner.PaperDoll)
        {
            if (myItems[slotB].name == "" || (int)myItems[slotB].equipSlot == slotA)
            {
                Item temp = myItems[slotB];
                myItems[slotB] = new Item(myDoll[slotA]);
                myDoll[slotA] = temp;

                dollChanged.Invoke(slotA);
                inventoryChanged.Invoke(slotB);
                return true;
            }
            else
            {
                print("B is not a new item, and it doesn't fit in that slot");
                return false;
            }
        }
        else if (dollSlotB == Slot.SlotOwner.PaperDoll)
        { 
            if ((int)myItems[slotA].equipSlot == slotB || myItems[slotA].name == "")
            {
                Item temp = myDoll[slotB];
                myDoll[slotB] = new Item(myItems[slotA]);
                myItems[slotA] = temp;

                dollChanged.Invoke(slotB);
                inventoryChanged.Invoke(slotA);
                return true;
            }
            else
            {
                print("A doesn't fit in that slot");
                return false;
            }
        }
        else
        {
            Item temp = myItems[slotB];
            myItems[slotB] = new Item(myItems[slotA]);
            myItems[slotA] = temp;

            inventoryChanged.Invoke(slotA);
            inventoryChanged.Invoke(slotB);
            return true;
        }
    }
    else
    {
        if ((slotA >= 0 && ((dollSlotA == Slot.SlotOwner.PaperDoll && slotA < paperDollSize) || (dollSlotA != Slot.SlotOwner.PaperDoll && slotA < inventorySize))) &&
        (slotB >= 0 && ((dollSlotB == Slot.SlotOwner.PaperDoll && slotB < paperDollSize) || (dollSlotB != Slot.SlotOwner.PaperDoll && slotB < inventorySize))))
        {
            print("slot number mismatch!");
        }

        return false;
    }
}*/
}
