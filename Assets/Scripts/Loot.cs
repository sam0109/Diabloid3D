using UnityEngine;
using System.Collections;

public class Loot : ItemHolder {

    new void Awake()
    {
        base.Awake();
        myType = InteractableType.Loot;
    }

    public override bool CanPlace(Item item, int slot, ItemHolder source)
    {
        if (items[slot].name == "")
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
        base.PlaceItem(item, slot, source);
    }

    public void Interacted()
    {
        OpenPopup();
    }
}
