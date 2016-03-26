using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public static Inventory inventory;
    public int inventorySize;
    public GameObject slot;
    public int slotWidth;
    public GameObject[] paperDoll;
    public RectTransform inventoryTransform;

    private List<Item> myItems { get; set; }
    private List<Slot> slots { get; set; }
    private EquipmentManager equipManager;

    // Use this for initialization
    void Awake()
    {
        inventory = this;
        myItems = new List<Item>();     //first, load/initialize the inventory. TODO: Load from file/network
        slots = new List<Slot>();
        for (int i = 0; i < inventorySize; i++)
        {
            myItems.Add(new Item());
        }
    }

    // Use this for initialization
    void Start()
    {
        myItems[9] = Database.myDatabase.GetItem("Sword3");  //temporary item maker for testing
        myItems[10] = Database.myDatabase.GetItem("Sword9");  //temporary item maker for testing
        myItems[11] = Database.myDatabase.GetItem("Helmet1");  //temporary item maker for testing

        int slotsSoFar = 0; //a running counter for how many slots have been used

        /*********************************/
        /*     create the paper doll     */
        /*********************************/

        foreach(GameObject slotObject in paperDoll)
        {
            Slot newSlot = slotObject.GetComponent<Slot>();
            newSlot.slotIndex = slotsSoFar;
            newSlot.SetImage(myItems[slotsSoFar].image);
            slots.Add(newSlot);
            slotsSoFar++;
        }

        /*********************************/
        /*    create the inventory UI    */
        /*********************************/
        float centeringOffset = (inventoryTransform.rect.width % slotWidth) / 2;
        int i = 0;
        for (; slotsSoFar < inventorySize; i += slotWidth)
        {
            for (int j = slotWidth; j < inventoryTransform.rect.width && slotsSoFar < inventorySize; j += slotWidth)
            {
                GameObject newSlotObject = (GameObject)Instantiate(slot, new Vector3(j + inventoryTransform.rect.xMin + centeringOffset, -i - centeringOffset, 0), Quaternion.identity);
                newSlotObject.transform.SetParent(inventoryTransform, false);
                Slot newSlot = newSlotObject.GetComponent<Slot>();
                newSlot.slotIndex = slotsSoFar;
                newSlot.SetImage(myItems[slotsSoFar].image);
                slots.Add(newSlot);
                slotsSoFar++;
            }
        }
        inventoryTransform.offsetMax = new Vector2(0, 0);
        inventoryTransform.offsetMin = new Vector2(0, -i - centeringOffset * 2);

        equipManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EquipmentManager>();
        equipManager.UpdateModels();
    }

    public bool SwapItems(int slotA, int slotB)
    {
        if(slotA < slots.Count && slotB < slots.Count && slotA >= 0 && slotB >= 0)
        {
            Item temp = myItems[slotB];
            myItems[slotB] = new Item(myItems[slotA]);
            myItems[slotA] = temp;
            slots[slotA].SetImage(myItems[slotA].image);
            slots[slotB].SetImage(myItems[slotB].image);

            if(slotA < paperDoll.Length || slotB < paperDoll.Length)
            {
                equipManager.UpdateModels();
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public Item GetItemInSlot(int slot)
    {
        if(slot >= 0 && slot < slots.Count)
        {
            return new Item(myItems[slot]);
        }
        else
        {
            return new Item();
        }
    }
}