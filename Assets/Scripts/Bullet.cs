using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	 
    void Start() {
		Destroy(gameObject, 3.0f);
    }
     
    void Update() {
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Enemy" || col.tag == "EnemyNoKill" || col.tag == "Player") {
			Destroy(gameObject);
		}
	}
}
