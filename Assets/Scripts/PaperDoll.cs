using UnityEngine;
using System.Collections.Generic;
using System;

public class PaperDoll : ItemHolder {

    new void Start()
    {
        base.Start();
        PlayerManager.playerManager.ResetHealth();
    }

    public override bool CanPlace(Item item, int slot)
    {
        if (slot >= 0 && slot < items.Count)
        {
            return true;
        }
        else
        {
            print("Invalid item index: " + slot);
            return false;
        }
    }
}
