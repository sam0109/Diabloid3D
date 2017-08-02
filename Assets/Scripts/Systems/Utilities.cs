using UnityEngine;
using System.Collections.Generic;

public static class Utilities{
    public static int slotWidth = 100;

	public static List<Slot> CreateSlotGrid(RectTransform inventoryTransform, int count, WindowDataSourceType myType, List<Item> items = null)
    {
        int slotsSoFar = 0; //a running counter for how many slots have been used
        List<Slot> uiSlots = new List<Slot>();
        float centeringOffset = (inventoryTransform.rect.width % slotWidth) / 2;
        int i = 0;
        for (; slotsSoFar < count; i += slotWidth)
        {
            for (int j = slotWidth; j < inventoryTransform.rect.width && slotsSoFar < count; j += slotWidth)
            {
                GameObject newSlotObject = (GameObject)Object.Instantiate(Prefabs.prefabs.slot, new Vector3(j + inventoryTransform.rect.xMin + centeringOffset, -i - centeringOffset, 0), Quaternion.identity);
                newSlotObject.transform.SetParent(inventoryTransform, false);
                Slot newSlot = newSlotObject.GetComponent<Slot>();
                newSlot.slotIndex = slotsSoFar;
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

    public static WindowType dataSourceToWindowType(WindowDataSourceType input)
    {
        switch (input)
        {
            case (WindowDataSourceType.PaperDoll):
                return WindowType.PaperDoll_Inventory;

            case (WindowDataSourceType.Inventory):
                return WindowType.PaperDoll_Inventory;

            case (WindowDataSourceType.Character):
                return WindowType.Character;

            case (WindowDataSourceType.Loot):
                return WindowType.Loot;

            case (WindowDataSourceType.Shop):
                return WindowType.Shop;

            default:
                Debug.LogWarning("Unknown window datasource type, returning PaperDoll_Inventory");
                return WindowType.PaperDoll_Inventory;
        }
    }
}
