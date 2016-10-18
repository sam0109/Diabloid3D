using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class SlotChanged : UnityEvent<int> { }

public abstract class ItemHolder : Interactable {
    public int itemCount;
    public SlotChanged slotChanged;
    public List<Item> items;

    public void Awake()
    {
        items = new List<Item>();
        if (slotChanged == null)
        {
            slotChanged = new SlotChanged();
        }
        for (int i = 0; i < itemCount; i++)
        {
            items.Add(new Item());
        }
    }

    public void Start()
    {
        slotChanged.Invoke(-1);
    }

    public virtual int ItemCount()
    {
        return items.Count;
    }

    public virtual Item GetItemInSlot(int slot)
    {
        if (slot >= 0 && slot < items.Count)
        {
            return new Item(items[slot]);
        }
        else
        {
            return new Item();
        }
    }

    public virtual List<Item> GetItemList()
    {
        return items;
    }

    public virtual bool CanPlace(Item item, int slot, ItemHolder source)
    {
        if (slot >= 0 && slot < items.Count)
        {
            return true;
        }
        else
        {
            print("Invalid item index: " + slot);
            return false;
        }
    }

    public virtual void PlaceItem(Item item, int slot, ItemHolder source)
    {
        items[slot] = item;
        slotChanged.Invoke(slot);
    }
}
