using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJumping : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	
	void OnTriggerEnter2D() {
		Destroy(gameObject);
		GameObject.FindWithTag("Player").GetComponent<Player>().ChangePlayerState(1);
	}
}
