using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

    private List<Slot> uiSlots;

    // Use this for initialization
    void Start () {
        uiSlots = Utilities.CreateSlotGrid(GetComponent<RectTransform>(), ItemManager.manager.myInventory.ItemCount(), Slot.SlotOwner.Inventory, ItemManager.manager.myInventory.GetItemList());
        ItemManager.manager.myInventory.slotChanged.AddListener(UpdateUI);
    }

    // Update is called once per frame
    void UpdateUI(int slot)
    {
        if(slot != -1)
        {
            uiSlots[slot].SetImage(ItemManager.manager.myInventory.GetItemInSlot(slot).image);
        }
        else
        {
            for(int i = 0; i < uiSlots.Count; i++)
            {
                uiSlots[i].SetImage(ItemManager.manager.myInventory.GetItemInSlot(i).image);
            }
        }
    }

    void OnDestroy()
    {
        print("clearing inventory");
        ItemManager.manager.myInventory.slotChanged.RemoveListener(UpdateUI);
    }
}
