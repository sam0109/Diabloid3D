using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {
    Text myText;
    // Use this for initialization
    void Start() {
        Inventory.inventory.currencyChanged += UpdateText;
        myText = GetComponent<Text>();
        UpdateText();
    }

    void UpdateText()
    {
        myText.text = "Gold: " + Inventory.inventory.gold;
    }
}
