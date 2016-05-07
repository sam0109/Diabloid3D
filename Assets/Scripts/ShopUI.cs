using UnityEngine;
using System.Collections;

public class ShopUI : MonoBehaviour {
    public Shop myShop;
    public RectTransform slotTransform;

    // Use this for initialization
    void Start () {
        Utilities.CreateSlotGrid(slotTransform, myShop.ItemCount(), Slot.SlotOwner.Shopkeeper, myShop.GetItemList());
	}

    // Update is called once per frame
    void Update () {
	
	}
}
