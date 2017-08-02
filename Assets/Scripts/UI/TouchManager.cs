using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[System.Serializable]
public class TouchCollisionEvent : UnityEvent<GameObject> { }

public class TouchManager : MonoBehaviour {
	/*
	public static TouchManager touchManager;
	public TouchCollisionEvent touchCollisionEvent;

	void Awake () {
		touchManager = this;
		if (touchCollisionEvent == null)
        {
            touchCollisionEvent = new TouchCollisionEvent();
        }
	}
	
	void Update () {
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began) 
			{
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				RaycastHit hit;
				if ((!GraphicRaycaster.Raycast(touch.position)) && Physics.Raycast(ray, out hit) && touchCollisionEvent != null) 
				{
					print("non graphic intercepted touch");
					touchCollisionEvent.Invoke(hit.collider.gameObject);
				}
			}
		}
	}*/
}