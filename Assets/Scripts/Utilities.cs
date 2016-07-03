using UnityEngine;
using System.Collections.Generic;

public static class Utilities{
    public static int slotWidth = 80;

	public static List<Slot> CreateSlotGrid(RectTransform inventoryTransform, int count, WindowType myType, List<Item> items = null)
    {
        int slotsSoFar = 0; //a running counter for how many slots have been used
        List<Slot> uiSlots = new List<Slot>();
        /*********************************/
        /*    create the inventory UI    */
        /*********************************/
        float centeringOffset = (inventoryTransform.rect.width % slotWidth) / 2;
        int i = 0;
        for (; slotsSoFar < count; i += slotWidth)
        {
            for (int j = slotWidth; j < inventoryTransform.rect.width && slotsSoFar < ItemManager.manager.myInventory.ItemCount(); j += slotWidth)
            {
                GameObject newSlotObject = (GameObject)Object.Instantiate(Prefabs.prefabs.slot, new Vector3(j + inventoryTransform.rect.xMin + centeringOffset, -i - centeringOffset, 0), Quaternion.identity);
                newSlotObject.transform.SetParent(inventoryTransform, false);
                Slot newSlot = newSlotObject.GetComponent<Slot>();
                newSlot.slotIndex = slotsSoFar;
                newSlot.myType = myType;
                if (items != null && items.Count > slotsSoFar)
                {
                    newSlot.SetImage(items[slotsSoFar].image);
                }
                uiSlots.Add(newSlot);
                slotsSoFar++;
            }
        }
        inventoryTransform.offsetMax = new Vector2(0, 0);
        inventoryTransform.offsetMin = new Vector2(0, -i - centeringOffset * 2);
        return uiSlots;
    }
}
