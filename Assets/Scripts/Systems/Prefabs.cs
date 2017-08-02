using UnityEngine;
using System.Collections.Generic;

public class Prefabs : MonoBehaviour {
    public static Prefabs prefabs;
    public GameObject slot;
    public GameObject inventory;
    public GameObject character;
    public GameObject shop;
    public GameObject loot;

    void Awake()
    {
        prefabs = this;
    }
}
