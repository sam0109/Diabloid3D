using UnityEngine;
using System.Collections.Generic;

public enum WindowType { PlayerInventory, PlayerDoll, Shop, Loot, PlayerStats};

public class WindowHandler : MonoBehaviour {
    public static WindowHandler popupHandler;
    Dictionary<GameObject, GameObject> myPopups;    //maps creators to their popups

    void Start () {
        myPopups = new Dictionary<GameObject, GameObject>();
        popupHandler = this;
	}

    public GameObject OpenWindowInPopup(GameObject creator, WindowType windowType, ItemHolder myHolder)
    {
        if (myPopups.ContainsKey(creator))
        {
            Destroy(myPopups[creator]);
            myPopups.Remove(creator);
        }
        switch (windowType)
        {
            case (WindowType.PlayerDoll):
                myPopups.Add(creator, MakePopup(Prefabs.prefabs.paperDoll, myHolder));
                break;
            case (WindowType.Loot):
                myPopups.Add(creator, MakePopup(Prefabs.prefabs.loot, myHolder));
                break;
            case (WindowType.PlayerInventory):
                myPopups.Add(creator, MakePopup(Prefabs.prefabs.inventory, myHolder));
                break;
            case (WindowType.Shop):
                myPopups.Add(creator, MakePopup(Prefabs.prefabs.shop, myHolder));
                break;
            case (WindowType.PlayerStats):
                myPopups.Add(creator, MakePopup(Prefabs.prefabs.character, myHolder));
                break;
            default:
                Debug.Log("Unknown WindowType");
                break;
        }
        return myPopups[creator];
    }

    GameObject MakePopup(GameObject newPopup, ItemHolder myItemHolder)
    {
        GameObject instantiatedPopup;

        if (newPopup == null)
        {
            Debug.Log("Null popup value passed to popup manager");
            return null;
        }
        else
        {
            instantiatedPopup = (GameObject)Instantiate(newPopup, GetComponent<RectTransform>().rect.center, Quaternion.identity);
            instantiatedPopup.transform.SetParent(gameObject.transform, false);
            ItemHolderUI holder = instantiatedPopup.GetComponent<ItemHolderUI>();
            if(holder != null)
            {
                holder.myItemHolder = myItemHolder;
            }
            return instantiatedPopup;
        }
    }
}