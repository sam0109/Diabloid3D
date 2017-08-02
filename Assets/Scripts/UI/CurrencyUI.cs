using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour {
    Text myText;
    // Use this for initialization
    void Start() {
        ItemManager.manager.playerCurrency.currencyChanged.AddListener(UpdateText);
        myText = GetComponent<Text>();
        UpdateText();
    }

    void UpdateText()
    {
        myText.text = "Gold: " + ItemManager.manager.playerCurrency.gold;
    }

    void OnDestroy()
    {
        ItemManager.manager.playerCurrency.currencyChanged.RemoveListener(UpdateText);
    }
}
