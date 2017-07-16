using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Shop : ItemHolder {

    new void Awake()
    {
        base.Awake();
        myType = InteractableType.Shop;
    }

    public override bool CanPlace(Item item, int slot, ItemHolder source)
    {
        if (base.CanPlace(item, slot, source) &&
            source.myType == InteractableType.Inventory &&
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

    public void Interacted()
    {
        OpenPopup();
    }

    private void OnMouseDown()
    {
        Interacted();
    }
}