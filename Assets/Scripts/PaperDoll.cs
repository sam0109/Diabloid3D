using UnityEngine;
using System.Collections.Generic;
using System;

public class PaperDoll : ItemHolder {

    new void Awake()
    {
        base.Awake();
    }

    new void Start()
    {
        base.Start();
        PlayerManager.playerManager.ResetHealth();
    }

    public override bool CanPlace(Item item, int slot, ItemHolder source)
    {
        if (base.CanPlace(item, slot, source) && (int) item.equipSlot == slot)
        {
            return true;
        }
        else
        {
            print("Invalid item index: " + slot);
            return false;
        }
    }

    public override void PlaceItem(Item item, int slot, ItemHolder source)
    {
        if (source.myType == InteractableType.Shop)
        {
            ItemManager.manager.playerCurrency.addGold(-item.price);
        }
        base.PlaceItem(item, slot, source);
    }
}
