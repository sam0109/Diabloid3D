using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Shop : ItemHolder {

    new void Awake()
    {
        base.Awake();
        myType = HolderType.Shop;
    }

    public override bool CanPlace(Item item, int slot, ItemHolder source)
    {
        if (base.CanPlace(item, slot, source) &&
            (source.myType == HolderType.PlayerInventory || source.myType == HolderType.PlayerDoll) &&
            items[slot].name == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void PlaceItem(Item item, int slot, ItemHolder source)
    {
        ItemManager.manager.myCurrency.addGold(item.price);
        base.PlaceItem(item, slot, source);
    }
}