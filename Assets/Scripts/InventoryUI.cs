﻿using UnityEngine;
using System.Collections;

public class InventoryUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Inventory.inventory.GenerateUI();
	}

    // Update is called once per frame
    void Update()
    {

    }
}
