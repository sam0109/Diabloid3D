using UnityEngine;
using System.Collections.Generic;

public abstract class ItemHolderUI : MonoBehaviour
{
    public ItemHolder myItemHolder;
    protected List<Slot> mySlots;
    protected Slot.SlotOwner myType;

    //note: we need to set myItemHolder in awake();

    public void Start()
    {
        if(myItemHolder == null)
        {
            print("Set myItemHolder!");
            return;
        }
        GenerateUI();
        myItemHolder.slotChanged.AddListener(UpdateUI);
    }

    // Update is called once per frame
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
