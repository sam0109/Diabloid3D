using UnityEngine;
using System.Collections;

public class ToggleEnable : MonoBehaviour {
    public GameObject toggle;

    public void Toggle()
    {
        if (toggle.activeSelf == true)
            toggle.SetActive(false);
        else
            toggle.SetActive(true);
    }
}
