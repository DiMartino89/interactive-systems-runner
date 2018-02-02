using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

	public float speed = 0.3f;
	public float rotation = 3.0f;
	
	private bool isFlipped;
	private Transform target;
	private Vector3 lastPos;
	private SpriteRenderer rend;
	
	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Player").transform;
		rend = GetComponent<SpriteRenderer>();
		lastPos = transform.position;
		
		// Avoid collision with other enemies
		Physics2D.IgnoreLayerCollision(9,9, true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Physics2D.IgnoreLayerCollision(9, 9);
		Physics2D.IgnoreLayerCollision(9, 10);
		
		// Follow the player with rotation when needed
		transform.LookAt(target.position);
		transform.Rotate(new Vector3(0,-90,0),Space.Self);
		transform.Translate(new Vector3(speed * Time.deltaTime * 3.0f,0,0) );
		
		Vector3 move = transform.position - lastPos;
		lastPos = transform.position;
		
		// Flip Enemy
		if(move.y < 0) {
			isFlipped = true;
		} else if(move.y > 0) {
			isFlipped = false;
		}
		
		flip();
	}
	
	void flip() {
		if(isFlipped == false) {
			rend.flipX = false;
		} else {
			rend.flipX = true;
		}
	}
	
	// Trigger-Function, if hit by player, deactivate the object
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerBullet") {
			gameObject.SetActive(false);
		}
	}
}
