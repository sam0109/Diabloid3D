using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// manages the display of items in a window
/// </summary>

public class ItemHolderUI : Window
{
    public ItemHolder myItemHolder;
    [SerializeField]
    protected List<Slot> mySlots;

    //note: we need to set myItemHolder in awake();

    public void InitializeItemHolder()
    {
        if(myItemHolder == null)
        {
            print("Set myItemHolder!");
            return;
        }
        GenerateUI();
        myItemHolder.slotChanged.AddListener(UpdateUI);
    }

    protected virtual void UpdateUI(int slot)
    {
        if (slot != -1)
        {
            mySlots[slot].SetImage(myItemHolder.GetItemInSlot(slot).image);
        }
        else
        {
            for (int i = 0; i < mySlots.Count; i++)
            {
                mySlots[i].SetImage(myItemHolder.GetItemInSlot(i).image);
            }
        }
    }

    protected virtual void GenerateUI()
    {
        mySlots = Utilities.CreateSlotGrid(GetComponent<RectTransform>(), myItemHolder.ItemCount(), myType, myItemHolder.GetItemList());
    }

    void OnDestroy()
    {
        myItemHolder.slotChanged.RemoveListener(UpdateUI);
    }
}