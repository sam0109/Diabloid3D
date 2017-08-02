﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class Inventory : ItemHolder
{
    new void Awake()
    {
        base.Awake();
        myType = WindowDataSourceType.Inventory;
    }

    new void Start()
    {
        items[9] = Database.myDatabase.GetItem("Sword3");  //temporary item maker for testing
        items[10] = Database.myDatabase.GetItem("Sword9");  //temporary item maker for testing
        items[11] = Database.myDatabase.GetItem("Helmet1");  //temporary item maker for testing
        base.Start();
    }

    public override bool CanPlace(Item item, int slot, ItemHolder source)
    {
        if (base.CanPlace(item, slot, source) &&
            (source.myType != WindowDataSourceType.Shop || ItemManager.manager.playerCurrency.gold >= item.price))
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
        if (source.myType == WindowDataSourceType.Shop)
        {
            ItemManager.manager.playerCurrency.addGold(-item.price);
        }
        base.PlaceItem(item, slot, source);
    }
}