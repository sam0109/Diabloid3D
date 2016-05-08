using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryUI : ItemHolderUI
{
    void Awake()
    {
        myItemHolder = ItemManager.manager.myInventory;
        myType = Slot.SlotOwner.Inventory;
    }
}
