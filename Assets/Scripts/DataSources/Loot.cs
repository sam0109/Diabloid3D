using UnityEngine;
using System.Collections;

public class Loot : ItemHolder {

    new void Awake()
    {
        itemCount = 9;
        base.Awake();
        myType = WindowDataSourceType.Loot;
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

    private void OnMouseDown()
    {
        WindowManager.windowManager.toggleWindowWithDataSource(this);
    }
}
