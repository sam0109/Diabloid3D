using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Database : MonoBehaviour {

    public List<Sprite> images;
    public List<GameObject> models;

    public static Database myDatabase;
    Dictionary<int, ItemInternal> itemTable;
    Dictionary<string, int> nameLookupTable;
    private string path;

    //{ None, Head, Body, Feet, Hands, Ring, LWeapon, RWeapon }
    public static List<List<EquipPoint>> EquipPointLookup = new List<List<EquipPoint>>
    {
        new List<EquipPoint>{}, //None
        new List<EquipPoint>{EquipPoint.Head}, //Head
        new List<EquipPoint>{EquipPoint.Body, EquipPoint.LShoulder, EquipPoint.RShoulder}, //body
        new List<EquipPoint>{}, //Feet
        new List<EquipPoint>{EquipPoint.LBracer, EquipPoint.RBracer}, //Hands
        new List<EquipPoint>{}, //Ring
        new List<EquipPoint>{EquipPoint.LWep}, //LWeapon
        new List<EquipPoint>{EquipPoint.RWep}  //RWeapon
    };

    // Use this for initialization
    void Awake () {
        myDatabase = this;
        itemTable = new Dictionary<int, ItemInternal>();
        nameLookupTable = new Dictionary<string, int>();
        path = Path.Combine(Application.persistentDataPath, "items.data");
        print(path);
        if (!LoadDatabase())
        {
            print("Failed to load items");
            ItemInternal newItem = new ItemInternal("Sword", 0, new int[1] { 0 }, EquipSlot.RWeapon);
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
                print("loaded " + newItem.myName);
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
public enum EquipSlot { None, Head, Body, Feet, Hands, Ring, LWeapon, RWeapon };

public class Item
{
    public Sprite image;
    public string name;
    public GameObject[] modelPrefabs;
    public EquipSlot equipSlot;

    public Item(Item toCopy)
    {
        name = toCopy.name;
        image = toCopy.image;
        modelPrefabs = toCopy.modelPrefabs;
        equipSlot = toCopy.equipSlot;
    }

    public Item()
    {
        name = "";
        image = null;
        modelPrefabs = null;
        equipSlot = EquipSlot.None;
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
        equipSlot = itemInternal.myEquipSlot;
    }
}

[System.Serializable]
public struct ItemInternal
{
    public int myImage;
    public int[] myModels;
    public string myName;
    public EquipSlot myEquipSlot;

    public ItemInternal(string name, int image, int[] models, EquipSlot equipSlot)
    {
        myName = name;
        myImage = image;
        myModels = models;
        myEquipSlot = equipSlot;
    }
}