using UnityEngine;
using System.Collections.Generic;

public class Window : MonoBehaviour {
	public WindowType myType;
    public void close(){
        Destroy(gameObject);
    }
}
