using System.Collections.Generic;
using UnityEngine;

public enum InteractableType {PaperDoll, Inventory, PlayerStats, Loot, Shop};

public abstract class Interactable : MonoBehaviour {
    public GameObject myPopup;
    public InteractableType myType {get; protected set;}
    
    public void OpenPopup()
    {
        if(!myPopup)
        switch (myType)
        {
            case (InteractableType.Loot):
                MakePopup(Prefabs.prefabs.loot);
                break;
            case (InteractableType.Inventory):
                MakePopup(Prefabs.prefabs.inventory);
                break;
            case (InteractableType.Shop):
                MakePopup(Prefabs.prefabs.shop);
                break;
            case (InteractableType.PlayerStats):
                MakePopup(Prefabs.prefabs.character);
                break;
            default:
                Debug.Log("Unknown WindowType");
                myPopup = null;
                break;
        }
    }

    private void MakePopup(GameObject newPopup)
    {
        if (newPopup == null)
        {
            Debug.Log("Null popup value passed to popup manager");
            myPopup = null;
        }
        else
        {
            GameObject UI = GameObject.FindGameObjectWithTag("UI");
            myPopup = Instantiate(newPopup, UI.GetComponent<RectTransform>().rect.center, Quaternion.identity);
            myPopup.transform.SetParent(UI.transform, false);
            ItemHolderUI[] holderUIs = myPopup.GetComponentsInChildren<ItemHolderUI>();
            foreach(ItemHolderUI holderUI in holderUIs)
            {
                if (holderUI.myItemHolder == null)
                {
                    holderUI.myItemHolder = (ItemHolder)this;
                }
                holderUI.InitializeItemHolder();
            }
        }
    }
}