using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	public Transform player;
	public GameObject mainCam;
	float camPos;
	float min;
	float max;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// Cam-center is +3 on x-axis from player
		camPos = player.transform.position.x + 3.0f;
		mainCam.transform.position = new Vector3(Mathf.Clamp(camPos, min, max), mainCam.transform.position.y, mainCam.transform.position.z);
	}
}
