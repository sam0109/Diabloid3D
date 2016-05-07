using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GoldManager : MonoBehaviour {
    public UnityEvent currencyChanged;
    public int gold;

    public void Awake()
    {
        if (currencyChanged == null)
        {
            currencyChanged = new UnityEvent();
        }
    }

    public void addGold(int amount)
    {
        gold += amount;
        if (gold < 0)
        {
            gold = 0;
        }
        currencyChanged.Invoke();
    }

    // Use this for initialization
    void Start () {
        currencyChanged.Invoke();
    }
}
