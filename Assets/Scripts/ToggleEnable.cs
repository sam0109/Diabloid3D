using UnityEngine;
using System.Collections;

public class ToggleEnable : MonoBehaviour {
    public GameObject toggle;
    public GameObject[] others;

    public void Toggle()
    {
        foreach(GameObject other in others)
        {
            other.SetActive(false);
        }

        if (toggle.activeSelf == true)
            toggle.SetActive(false);
        else
            toggle.SetActive(true);
    }
}
