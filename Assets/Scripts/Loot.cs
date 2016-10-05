using UnityEngine;
using System.Collections;

public class Loot : ItemHolder {

    new void Awake()
    {
        base.Awake();
        myType = HolderType.Loot;
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
        GameObject myPopup = WindowHandler.popupHandler.OpenWindowInPopup(gameObject, HolderType.Loot, this);
        myPopup.GetComponentInChildren<ItemHolderUI>().myItemHolder = GetComponent<Shop>();
    }
}
