using UnityEngine;
using System.Collections;

public class OpenInventory : MonoBehaviour {
    GameObject myPopup;
	public void OpenInventoryInPopup()
    {
        if(myPopup)
        {
            Destroy(myPopup);
        }
        myPopup = PopupHandler.popupHandler.MakePopup(Prefabs.prefabs.inventory);
    }

    public void OpenPaperDollInPopup()
    {
        if (myPopup)
        {
            Destroy(myPopup);
        }
        myPopup = PopupHandler.popupHandler.MakePopup(Prefabs.prefabs.paperDoll);
    }
}
