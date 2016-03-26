using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    public Color minColor;
    public Color maxColor;

    Image image_target;

    // Use this for initialization
    void Start () {
        image_target = gameObject.GetComponent<Image>();
        SetHealth(1);
    }
	
    public void SetHealth(float healthNormalized)
    {
        transform.localScale = new Vector3(healthNormalized, 1, 1);
        image_target.color = Color.Lerp(minColor, maxColor, transform.localScale.x);
    }
}