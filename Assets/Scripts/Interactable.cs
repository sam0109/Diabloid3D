using UnityEngine;

public enum InteractableType {PlayerDoll, PlayerInventory, PlayerStats, Loot, Shop};

public abstract class Interactable : MonoBehaviour {
    public GameObject myPopup;
    public InteractableType myType {get; protected set;}
    
    public void OpenPopup()
    {
        if(!myPopup)
        switch (myType)
        {
            case (InteractableType.PlayerDoll):
                MakePopup(Prefabs.prefabs.paperDoll);
                break;
            case (InteractableType.Loot):
                MakePopup(Prefabs.prefabs.loot);
                break;
            case (InteractableType.PlayerInventory):
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
            myPopup = (GameObject)Instantiate(newPopup, UI.GetComponent<RectTransform>().rect.center, Quaternion.identity);
            myPopup.transform.SetParent(UI.transform, false);
            ItemHolderUI holder = myPopup.GetComponentInChildren<ItemHolderUI>();
            if(holder != null)
            {
                holder.myItemHolder = (ItemHolder)this;
                holder.InitializeItemHolder();
            }
        }
    }
}