using UnityEngine;
using System.Collections;

public class Prefabs : MonoBehaviour {
    public static Prefabs prefabs;
    public GameObject slot;
    public GameObject inventory;
    public GameObject paperDoll;
    public GameObject shop;

    void Awake()
    {
        prefabs = this;
    }
}
