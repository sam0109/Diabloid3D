using UnityEngine;
using System.Collections;
using System;

public class ShopUI : ItemHolderUI
{
    void Awake()
    {
        myType = Slot.SlotOwner.Shopkeeper;
    }
}
