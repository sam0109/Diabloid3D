using UnityEngine;
using System.Collections;

public class PopupHandler : MonoBehaviour {
    public static PopupHandler popupHandler;
    public GameObject popup;
	// Use this for initialization
	void Start () {
        popupHandler = this;
	}
    public GameObject MakePopup()
    {
        GameObject newPopup = (GameObject)Instantiate(popup, GetComponent<RectTransform>().rect.center, Quaternion.identity);
        newPopup.transform.SetParent(gameObject.transform, false);
        return newPopup;
    }
}