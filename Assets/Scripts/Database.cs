using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Database : MonoBehaviour {

    public List<Sprite> images;
    public List<GameObject> models;

    public static Database myDatabase;
    Dictionary<int, ItemInternal> itemTable;
    Dictionary<string, int> nameLookupTable;
    private string path;

    //{ Head, Body, Feet, Hands, Ring, LWeapon, RWeapon, None }
    public static List<List<EquipPoint>> EquipPointLookup = new List<List<EquipPoint>>
    {
        new List<EquipPoint>{EquipPoint.Head}, //Head
        new List<EquipPoint>{EquipPoint.Body, EquipPoint.LShoulder, EquipPoint.RShoulder}, //body
        new List<EquipPoint>{}, //Feet
        new List<EquipPoint>{EquipPoint.LBracer, EquipPoint.RBracer}, //Hands
        new List<EquipPoint>{}, //Ring
        new List<EquipPoint>{EquipPoint.LWep}, //LWeapon
        new List<EquipPoint>{EquipPoint.RWep},  //RWeapon
        new List<EquipPoint>{} //None
    };

    // Use this for initialization
    void Awake ()
    {
        myDatabase = this;
        itemTable = new Dictionary<int, ItemInternal>();
        nameLookupTable = new Dictionary<string, int>();
        path = Path.Combine(Application.persistentDataPath, "items.data");
        print(path);
        print(path);
        if (!LoadDatabase())
        {
            print("Failed to load items");
            ItemInternal newItem = new ItemInternal("Sword1", 0, new int[1] { 0 }, EquipSlot.RWeapon, ItemType.Sword, new int[4] { 1, 2, 3, 4 }, 10);
            itemTable.Add(0, newItem);
            nameLookupTable.Add(newItem.myName, 0);
        }

    }

    public Item GetItem(string name)
    {
        try
        {
            return new Item(itemTable[nameLookupTable[name]]);
        }
        catch
        {
            return new Item();
        }
    }

    public bool LoadDatabase()
    {
        print("Loading database");
        try {
            string[] lines = File.ReadAllLines(path);

            for(int i = 0; i < lines.Length; i++)
            {
                ItemInternal newItem = JsonUtility.FromJson<ItemInternal>(lines[i]);
                itemTable.Add(i, newItem);
                nameLookupTable.Add(newItem.myName, i);
                print("loaded " + newItem.myName + "Costs:" + newItem.myPrice);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SaveDatabase()
    {
        print("Saving database");
        try
        {
            StreamWriter stream = new StreamWriter(path);
            foreach (ItemInternal item in itemTable.Values)
            {
                print("Writing " + item.myName + " to database.");
                stream.WriteLine(JsonUtility.ToJson(item));
            }
            stream.Close();
            return true;
        }
        catch
        {
            print("failed to save database");
            return false;
        }
    }
}

public enum EquipPoint { Head, LBracer, RBracer, LShoulder, RShoulder, LWep, RWep, Body };
public enum EquipSlot { Head, Body, Feet, Hands, Ring, LWeapon, RWeapon, None };
public enum ItemType { Bow, Sword, Axe, Hammer, Helmet, Body, Feet, Hands, Ring, Shield, None };

public class Item
{
    public Sprite image;
    public string name;
    public GameObject[] modelPrefabs;
    public EquipSlot equipSlot;
    public ItemType itemType;
    public int price;

    //Weapon properties
    public int damage;
    public int speed;
    public int range;
    //Armor properties
    public int strength, intelligence, agility;
    public int armor;

    public Item(Item toCopy)
    {
        name = toCopy.name;
        image = toCopy.image;
        modelPrefabs = toCopy.modelPrefabs;
        equipSlot = toCopy.equipSlot;
        itemType = toCopy.itemType;
        strength = toCopy.strength;
        intelligence = toCopy.intelligence;
        agility = toCopy.agility;
        armor = toCopy.armor;
        damage = toCopy.damage;
        speed = toCopy.speed;
        range = toCopy.range;
        price = toCopy.price;
    }

    public Item()
    {
        name = "";
        image = null;
        modelPrefabs = null;
        equipSlot = EquipSlot.None;
        itemType = ItemType.None;
        strength = intelligence = agility = armor = damage = speed = range = price = 0;
    }

    public Item(ItemInternal itemInternal)
    {
        name = itemInternal.myName;
        image = Database.myDatabase.images[itemInternal.myImage];
        modelPrefabs = new GameObject[itemInternal.myModels.Length];
        for (int i = 0; i < itemInternal.myModels.Length; i++)
        {
            modelPrefabs[i] = Database.myDatabase.models[itemInternal.myModels[i]];
        }
        price = itemInternal.myPrice;
        equipSlot = itemInternal.myEquipSlot;
        itemType = itemInternal.myItemType;
        switch (itemType)
        {
            case (ItemType.Bow):
            case (ItemType.Axe):
            case (ItemType.Hammer):
            case (ItemType.Sword):
                damage = itemInternal.myItemProperties[0];
                speed = itemInternal.myItemProperties[1];
                range = itemInternal.myItemProperties[2];
                strength = intelligence = agility = armor = 0;
                break;
            case (ItemType.Body):
            case (ItemType.Feet):
            case (ItemType.Hands):
            case (ItemType.Helmet):
            case (ItemType.Ring):
            case (ItemType.Shield):
                strength = itemInternal.myItemProperties[0];
                intelligence = itemInternal.myItemProperties[1];
                agility = itemInternal.myItemProperties[2];
                armor = itemInternal.myItemProperties[3];
                damage = speed = range = 0;
                break;
            case (ItemType.None):
                strength = intelligence = agility = armor = damage = speed = range = 0;
                Debug.Log("No item type! Cannot initialize fields");
                break;
            default:
                strength = intelligence = agility = armor = damage = speed = range = 0;
                Debug.Log("Unrecognized item type");
                break;
        }
    }
}

[System.Serializable]
public struct ItemInternal
{
    public int myImage;
    public int[] myModels;
    public string myName;
    public EquipSlot myEquipSlot;
    public ItemType myItemType;
    public int[] myItemProperties;
    public int myPrice;

    public ItemInternal(string name, int image, int[] models, EquipSlot equipSlot, ItemType itemType, int[] itemProperties, int price)
    {
        myName = name;
        myImage = image;
        myModels = models;
        myEquipSlot = equipSlot;
        myItemType = itemType;
        myItemProperties = itemProperties;
        myPrice = price;
    }
}

[System.Serializable]
public class CharacterStat
{
    public int strength, agility, intelligence, level;
}