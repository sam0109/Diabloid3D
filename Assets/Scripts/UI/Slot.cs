using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    public int slotIndex;
    public GameObject item;
    private Image myItem;

	// Use this for initialization
	void Awake ()
    {
        GameObject myItemObject = Instantiate(item, Vector3.zero, Quaternion.identity);
        myItemObject.transform.SetParent(transform, false);
        myItem = myItemObject.GetComponent<Image>();
	}

    public void SetImage (Sprite newImage)
    {
        if (newImage == null)
        {
            myItem.enabled = false;
        }
        else
        {
            myItem.enabled = true;
            myItem.sprite = newImage;
        }
    }
}
