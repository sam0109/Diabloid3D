using UnityEngine;
using System.Collections.Generic;

public enum HolderType { PlayerInventory, PlayerDoll, Shop, Loot, PlayerStats};

public class WindowHandler : MonoBehaviour {
    public static WindowHandler popupHandler;

    void Start () {
        popupHandler = this;
	}

    public GameObject OpenWindowInPopup(GameObject creator, HolderType windowType, ItemHolder myHolder)
    {
        GameObject popup = null;
        switch (windowType)
        {
            case (HolderType.PlayerDoll):
                popup = MakePopup(Prefabs.prefabs.paperDoll, myHolder);
                break;
            case (HolderType.Loot):
                popup = MakePopup(Prefabs.prefabs.loot, myHolder);
                break;
            case (HolderType.PlayerInventory):
                popup = MakePopup(Prefabs.prefabs.inventory, myHolder);
                break;
            case (HolderType.Shop):
                popup = MakePopup(Prefabs.prefabs.shop, myHolder);
                break;
            case (HolderType.PlayerStats):
                popup = MakePopup(Prefabs.prefabs.character, myHolder);
                break;
            default:
                Debug.Log("Unknown WindowType");
                break;
        }
        return popup;
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
            ItemHolderUI holder = instantiatedPopup.GetComponentInChildren<ItemHolderUI>();
            if(holder != null)
            {
                holder.myItemHolder = myItemHolder;
                holder.InitializeItemHolder();
            }
            return instantiatedPopup;
        }
    }
}