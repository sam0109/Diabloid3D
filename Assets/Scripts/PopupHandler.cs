using UnityEngine;
using System.Collections;

public class PopupHandler : MonoBehaviour {
    public static PopupHandler popupHandler;
    public GameObject popup;
	// Use this for initialization
	void Start () {
        popupHandler = this;
	}
    public GameObject MakePopup(GameObject newPopup)
    {
        GameObject instantiatedPopup;

        if (newPopup == null)
        {
            instantiatedPopup = (GameObject)Instantiate(popup, GetComponent<RectTransform>().rect.center, Quaternion.identity);
        }

        else
        {
            instantiatedPopup = (GameObject)Instantiate(newPopup, GetComponent<RectTransform>().rect.center, Quaternion.identity);
        }

        instantiatedPopup.transform.SetParent(gameObject.transform, false);
        return instantiatedPopup;
    }
}