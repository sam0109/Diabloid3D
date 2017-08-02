using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// manages the display of items in a window
/// </summary>

public class ItemHolderUI : WindowComponent
{
    public ItemHolder myItemHolder;
    [SerializeField]
    protected List<Slot> mySlots;

    public override void InitializeComponent()
    {
        if(myDataSource == null){
            Debug.LogError("myDataSource is null!");
            return;
        }
        myItemHolder = (ItemHolder) myDataSource;
        if(myItemHolder == null){
            Debug.LogError("myDataSource is not of the ItemHolderType!");
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