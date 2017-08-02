using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Shop : ItemHolder {

    new void Awake()
    {
        base.Awake();
        myType = WindowDataSourceType.Shop;
    }

    public override bool CanPlace(Item item, int slot, ItemHolder source)
    {
        if (base.CanPlace(item, slot, source) &&
            (source.myType == WindowDataSourceType.Inventory || source.myType == WindowDataSourceType.PaperDoll) &&
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
        ItemManager.manager.playerCurrency.addGold(item.price);
        base.PlaceItem(item, slot, source);
    }

    public void Touched()
    {
        WindowManager.windowManager.toggleWindowWithDataSource(this);
    }
}