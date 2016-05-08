using UnityEngine;
using System.Collections.Generic;

public class paperDollUI : ItemHolderUI
{
    [SerializeField]
    private List<Slot> paperDollSlots;

    void Awake()
    {
        myItemHolder = ItemManager.manager.myDoll;
        myType = Slot.SlotOwner.PaperDoll;
    }

    protected override void UpdateUI(int slot)
    {
        if (slot != -1)
        {
            paperDollSlots[slot].SetImage(ItemManager.manager.myDoll.GetItemInSlot(slot).image);
        }
        else
        {
            for (int i = 0; i < paperDollSlots.Count; i++)
            {
                paperDollSlots[i].SetImage(ItemManager.manager.myDoll.GetItemInSlot(i).image);
            }
        }
    }
	
	// Update is called once per frame
	protected override void GenerateUI () {
        List<Item> items = ItemManager.manager.myDoll.GetItemList();

        /*********************************/
        /*    populate the paper doll    */
        /*********************************/

        for (int i = 0; i < ItemManager.manager.myDoll.ItemCount(); i++)
        {
            paperDollSlots[i].slotIndex = i;
            paperDollSlots[i].myOwner = Slot.SlotOwner.PaperDoll;
            paperDollSlots[i].SetImage(items[i].image);
        }
    }
}
