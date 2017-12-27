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
		//get all the components needed
		mainBody = GetComponentInParent<Rigidbody2D> ();
		rend = GetComponentInParent<SpriteRenderer> ();
		anim = GetComponentInParent<Animator> ();
		player = GameObject.Find("Player");
		
		Physics2D.IgnoreLayerCollision(9,9, true);
	}
	
	// Update is called once per frame
	void Update () {
		//if debug mode is enabled, ignore the jumptimer and jump when Z is pressed
		if (debugMode == true) {
			if (Input.GetKeyDown (KeyCode.Z)) {
				hasJumped = true;
			}
		} else {
			//if slime is not dead allow everything to run
			if (isDead == false) {
				//timer that counts up for jump
				jumpTimer += Time.deltaTime;

				//make the AI jump when the timer reaches max
				if (jumpTimer >= jumpTime) {
					hasJumped = true;
				}
			}
		}

		//run the jump function
		if (hasJumped == true) {
			Jump ();
		}
	}

	void Jump(){
		if (hasJumped == true) {
			//find the direction of the player
			Vector2 direction = transform.position - player.transform.position;
			//find the distance of the player
			float distance = Vector2.Distance (transform.position, player.transform.position);

			//timer of the about to jump animation before the actual jump
			speedTimer += Time.deltaTime;

			if (speedTimer >= jumpSpeed) {
				//jump up
				mainBody.AddForce (Vector2.up * jumpDist);

				//if the player is within range, check to see what direction he is then jump towards the player
				if (direction.x > 0 && distance < playerDetectRange) {
					//jump left at 1/3 the force of the main jump
					mainBody.AddForce (Vector2.left * (jumpDist / 3));
					//do not flip the image on the X
					rend.flipX = false;

				} else if (direction.x < 0 && distance < playerDetectRange) {
					//jump right at 1/3 the force of the main jump
					mainBody.AddForce (Vector2.right * (jumpDist / 3));
					//flip the image on the x
					rend.flipX = true;

					//If the player is not within range
				} else {
					//get a random value and jump in a random direction 50/50
					if (Random.value > .50f) {
						//jump right at 1/3 the force of the main jump
						mainBody.AddForce (Vector2.right * (jumpDist / 3));
						//flip the image on the x
						rend.flipX = true;
					} else {
						//jump left at 1/3 the force of the main jump
						mainBody.AddForce (Vector2.left * (jumpDist / 3));
						//do not flip the image on the X
						rend.flipX = false;
					}
				}

				//reset the animation, speedTimer, jumpTimer and hasJumped
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
