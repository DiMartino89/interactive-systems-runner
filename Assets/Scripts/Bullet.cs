using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	// Use this for initialization	
    void Start() {
		// Destroy Bullet after 3 seconds if nothing to hit
		Destroy(gameObject, 3.0f);
    }
    
	// Update is called once per frame
    void Update() {
	}
	
	// Trigger-Function for Bullet
	void OnTriggerEnter2D(Collider2D col) {
		// If Bullet (from Player or Enemy) hits target, destroy it
		if(col.tag == "Enemy" || col.tag == "EnemyNoKill" || col.tag == "Player") {
			Destroy(gameObject);
		}
	}
}
