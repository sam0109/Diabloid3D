﻿using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroyMeNow()
    {
        Destroy(gameObject);
    }
}
