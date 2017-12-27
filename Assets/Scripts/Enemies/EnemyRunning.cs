using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunning : MonoBehaviour {

	public float speed = 2.0f;
	public LayerMask EnemyMask;
	
	Transform myTrans;
	float myWidth, myHeight;
	bool isBlocked;
	Rigidbody2D rb;
	
	// Use this for initialization
	void Start () {
		// Initialize Variables
		myTrans = this.transform;
		rb = GetComponent<Rigidbody2D>();
		isBlocked = false;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Ignore Collision between Enemy (Layer 8) and Levelboundries (Layer 9)
		Physics2D.IgnoreLayerCollision(9, 9);
		Physics2D.IgnoreLayerCollision(9, 10);
		
		// Turn Enemy if obstacle reached
		if(isBlocked) {
			Vector2 currRot = myTrans.eulerAngles;
			currRot.y += 180;
			myTrans.eulerAngles = currRot;
			isBlocked = false;
		}
		
		// Velocity of enemy rigidbody
		Vector2 myVel = rb.velocity;
		myVel.x = -(myTrans.right.x) * speed;
		rb.velocity = myVel;
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Wall") {
			isBlocked = true;
		}
	}
}
