using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumping : MonoBehaviour {

	public bool debugMode;
	
	private Rigidbody2D mainBody;
	private GameObject player;
	private SpriteRenderer rend;
	private Animator anim;

	public float jumpDist = 300f;
	public float jumpTime = 4f;
	private float jumpTimer;
	public float jumpSpeed = 1f;
	private float speedTimer;

	public float playerDetectRange = 10f;

	private bool hasJumped;

	private bool isDead;

	// Use this for initialization
	void Start () {
		
		// get all the components needed
		mainBody = GetComponentInParent<Rigidbody2D> ();
		rend = GetComponentInParent<SpriteRenderer> ();
		anim = GetComponentInParent<Animator> ();
		player = GameObject.Find("Player");
		
		// ignore collision with other enemies
		Physics2D.IgnoreLayerCollision(9,9, true);
	}
	
	// Update is called once per frame
	void Update () {
		
		// only debug mode
		if (debugMode == true) {
			if (Input.GetKeyDown (KeyCode.Z)) {
				hasJumped = true;
			}
		} else {
			if (isDead == false) {
				// timer that counts up for next jump
				jumpTimer += Time.deltaTime;

				if (jumpTimer >= jumpTime) {
					hasJumped = true;
				}
			}
		}

		if (hasJumped == true) {
			Jump ();
		}
	}

	void Jump(){
		if (hasJumped == true) {
			
			// direction and distance of he player
			Vector2 direction = transform.position - player.transform.position;
			float distance = Vector2.Distance (transform.position, player.transform.position);

			speedTimer += Time.deltaTime;

			// jump up
			if (speedTimer >= jumpSpeed) {
				mainBody.AddForce (Vector2.up * jumpDist);

				// If the player is within the range, jump towards the player
				if (direction.x > 0 && distance < playerDetectRange) {
					// jump left with 30% of the jump-force but don't flip
					mainBody.AddForce (Vector2.left * (jumpDist / 3));
					rend.flipX = false;

				} else if (direction.x < 0 && distance < playerDetectRange) {
					// jump right with 30% of the jump-force and flip
					mainBody.AddForce (Vector2.right * (jumpDist / 3));
					rend.flipX = true;

				// If the player is not within range jump random 50/50
				} else {
					if (Random.value > .50f) {
						// jump right
						mainBody.AddForce (Vector2.right * (jumpDist / 3));
						rend.flipX = true;
					} else {
						// jump left
						mainBody.AddForce (Vector2.left * (jumpDist / 3));
						rend.flipX = false;
					}
				}

				// reset params
				speedTimer = 0;
				jumpTimer = 0;
				hasJumped = false;

			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerBullet") {
			gameObject.SetActive(false);
		}
	}
}
