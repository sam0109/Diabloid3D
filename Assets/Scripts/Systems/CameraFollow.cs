using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Vector3 offset;
    GameObject player;

	// Use this for initialization
	void Start () {
	player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
	Vector3 playerPos = player.transform.position;
	transform.position = new Vector3(playerPos.x + offset.x, playerPos.y + offset.y, playerPos.z + offset.z);
	}
}
