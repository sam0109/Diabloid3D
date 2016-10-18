using UnityEngine;
using System.Collections.Generic;

public class paperDollUI : ItemHolderUI
{
    void Awake()
    {
        myItemHolder = ItemManager.manager.myDoll;
        myType = InteractableType.PlayerDoll;
    }
	
	protected override void GenerateUI () {
        List<Item> items = myItemHolder.GetItemList();

        //    populate the paper doll

        for (int i = 0; i < myItemHolder.ItemCount(); i++)
        {
            mySlots[i].slotIndex = i;
            mySlots[i].SetImage(items[i].image);
        }
    }
}
