using UnityEngine;
using System.Collections;

//necessary because buttons cannot do multiple arguments in unity. Should replace with generic script.
public class OpenWindowPassthrough : MonoBehaviour {
    public WindowType myWindowType;
    public GameObject ownerObject;

	public void OpenWindow () {
        ItemHolder[] holders = ownerObject.GetComponents<ItemHolder>();
        foreach (ItemHolder holder in holders)
        {
            if(holder.myType == myWindowType)
            {
                WindowHandler.popupHandler.OpenWindowInPopup(ownerObject, myWindowType, holder);
                return;
            }
            Debug.Log("No such holder found on owner object");
        }
	}
}
