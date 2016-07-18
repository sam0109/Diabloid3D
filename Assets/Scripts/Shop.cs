﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Shop : ItemHolder {

    new void Awake()
    {
        base.Awake();
        myType = WindowType.Shop;
    }

    public override bool CanPlace(Item item, int slot, ItemHolder source)
    {
        if (base.CanPlace(item, slot, source) &&
            (source.myType == WindowType.PlayerInventory || source.myType == WindowType.PlayerDoll) &&
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

    public void Interacted()
    {
        GameObject myPopup = WindowHandler.popupHandler.OpenWindowInPopup(gameObject, WindowType.Shop, this);
        myPopup.GetComponentInChildren<ItemHolderUI>().myItemHolder = GetComponent<Shop>();
    }
}