using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float speed = 5f;
	public float jumpHeight = 300f;
	public float fireRate = 2.0f;
	public float nextFire = 1.0f;
	public int lifes = 3;
	
	public bool grounded = true;
	
	public GameObject bullet;
	public GameObject winGame;
	public GameObject gameOver;
	public Button backToMenu;
	public Text currLifes;
	public Text currAmmo;
	public Text currInvulnerability;
	public Text currJumping;
	public Text currStat;
	public Text currDifficulty;
	
	private bool isFlipped;
	private bool isDead;
	private bool isFinished;
	private bool isTriggered = false; 
	private SpriteRenderer rend, rendHead, rendBody, rendLegs;
	private Rigidbody2D player;
	private static int playerStat;
	private float timeToRespawn;
	private float timeReinforcedJump;
	private float invulnerability;
	private int ammo;
	private Vector2 lastCheckpoint;
	Vector2 bulletPos;
	GameObject clone;
	Behaviour halo;
	
	/*void Awake() {
		DontDestroyOnLoad(this);
	}*/
	
	// Use this for initialization
	void Start () {
		// Initialize all player-properties
		player = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
		rendHead = transform.Find("editedLook(Clone)").GetComponent<SpriteRenderer>();
		rendBody = transform.Find("editedLook(Clone)").GetChild(0).GetComponent<SpriteRenderer>();
		rendLegs = transform.Find("editedLook(Clone)").GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
		halo =(Behaviour)GetComponent("Halo");
		lastCheckpoint = transform.position;
		isDead = false;
		playerStat = 0;
		ammo = 0;
		invulnerability = 0;
		timeReinforcedJump = 0.0f;
		currLifes.text = "♥ " + lifes;
		currAmmo.text = "Ammo: " + ammo;
		currInvulnerability.text = "Inv: " + Math.Round(invulnerability, 1) + "s";
		currJumping.text = "Jump: " + Math.Round(timeReinforcedJump, 1);
		currDifficulty.text = "Diff: " + LevelGeneration.difficulty;
		GetComponent<AnimationController>().ChangeState(0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isDead == false && isFinished == false) {
			// Player-Controls
			
			// walk-right with animation
			if(Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D))) {
				transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
				GetComponent<AnimationController>().ChangeState(1);
			}
			
			// To stop walk-right animation
			if(Input.GetKeyUp(KeyCode.RightArrow) || (Input.GetKeyUp(KeyCode.D))) {
				GetComponent<AnimationController>().ChangeState(0);
			}
			
			// walk-left with animation
			if(Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A))) {
				transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
				GetComponent<AnimationController>().ChangeState(2);
			}
			
			// To stop walk-left animation
			if(Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A))) {
				GetComponent<AnimationController>().ChangeState(0);
			}
			
			// Jump
			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				if(timeReinforcedJump < Time.time && grounded == true) {
					player.velocity = new Vector2(0, jumpHeight);
				} else if(timeReinforcedJump > Time.time && grounded == true) {
					player.velocity = new Vector2(0, jumpHeight * 1.2f);
				}
			}
			
			// Shooting
			if(Input.GetKeyDown(KeyCode.Space)) {	
				if(Time.time > nextFire && ammo > 0) {
					nextFire = nextFire + fireRate;
					fire();
					Rigidbody2D cloneRB = clone.GetComponent<Rigidbody2D>();
					if(isFlipped == false) {
						cloneRB.velocity = new Vector2(5.0f, 0);
					} else {
						cloneRB.velocity = new Vector2(-5.0f, 0);
					}
					ammo--;
				}
			}
			
			// Flip player
			float move = Input.GetAxis("Horizontal");
			
			if(move < 0) {
				isFlipped = true;
			} else if(move > 0) {
				isFlipped = false;
			}
			
			flip();
		}
		
		// Reset high-jump-time
		if(timeReinforcedJump < Time.time) {
			timeReinforcedJump = 0;
			currJumping.text = "Jump: " + 0 + "s";
		}
		
		// Reset invulnerability-time
		if(invulnerability < Time.time) {
			invulnerability = 0;
			currInvulnerability.text = "Inv: " + 0 + "s";
			Physics2D.IgnoreLayerCollision(8,9, false);
			halo.enabled = false;
		}
		
		// Respawn player after death
		if(isDead == true && lifes > 0 && timeToRespawn < Time.time) {
			transform.position = lastCheckpoint;
			rend.enabled = true;
			rendHead.enabled = true;
			rendBody.enabled = true;
			rendLegs.enabled = true;
			Physics2D.IgnoreLayerCollision(8,9, false);
			isDead = false;
			isTriggered = false;
		}
		
		// Set GameOver after third death
		if(lifes == 0) {	
			Physics2D.IgnoreLayerCollision(8,9, true);
			gameOver.SetActive(true);
			backToMenu.gameObject.SetActive(true);
		}
		
		// Update property-labels
		currLifes.text = "♥ " + lifes;
		currAmmo.text = "Ammo: " + ammo;
		currStat.text = "Stat: " + playerStat;
		if(Time.time < invulnerability) {
			currInvulnerability.text = "Inv: " + Math.Round((invulnerability - Time.time), 1) + "s";
		}
		if(Time.time < timeReinforcedJump) {
			currJumping.text = "Jump: " + Math.Round((timeReinforcedJump - Time.time), 1) + "s";
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		
		grounded = true;
		
		// If player hits enemy bullets or killing-enemies = death
		if(col.tag == "EnemyBullet" || col.tag == "Enemy") {
			if(isTriggered == false) { 
				lifes--;
				Physics2D.IgnoreLayerCollision(8,9, false);
				if(lifes > 0) {
					timeToRespawn = Time.time + 2;
				}
				isDead = true;
				col.gameObject.SetActive(false);
				rend.enabled = false;
				rendHead.enabled = false;
				rendBody.enabled = false;
				rendLegs.enabled = false;
				isTriggered = true;
				grounded = false;
			}
		}
		
		// If player hits checkpoint, recognize this position
		if(col.tag == "Checkpoint") {
			lastCheckpoint = transform.position;
			grounded = false;
		}
		
		// If player hits FallDownCatch = death
		if(col.tag == "FallDownCatch") {
			if(isTriggered == false) { 
				lifes--;
				Physics2D.IgnoreLayerCollision(8,9, false);
				if(lifes > 0) {
					timeToRespawn = Time.time + 2;
				}
				isDead = true;
				rend.enabled = false;
				rendHead.enabled = false;
				rendBody.enabled = false;
				rendLegs.enabled = false;
				isTriggered = true;
				grounded = false;
			}
		}
		
		// If player hits finish, set Win
		if (col.tag == "Finish") {			
			winGame.SetActive(true);
			isFinished = true;
			rend.enabled = false;
			Physics2D.IgnoreLayerCollision(8,9, false);
			backToMenu.gameObject.SetActive(true);
		}
	}
	
	// If player collider stays in contact with ground
	void OnTriggerStay2D(Collider2D col) {
		grounded = true;    
	}
	
	// If player jumps
	void OnTriggerExit2D(Collider2D col) {
		grounded = false;
	}
	
	// Fire-Function for player left and right
	void fire() {
		bulletPos = transform.position;
		if(isFlipped == false) {
			bulletPos.x += 1.0f;
		} else {
			bulletPos.x -= 1.0f;
		}
		clone = Instantiate(bullet, bulletPos, Quaternion.identity) as GameObject;
	}
	
	// Flip the player-sprite
	void flip() {
		if(isFlipped == false) {
			rend.flipX = false;
		} else {
			rend.flipX = true;
		}
	}
	
	// Change the player-State when he hits an Item
	public void ChangePlayerState(int state) {
		playerStat = state;
		if(playerStat == 1 && timeReinforcedJump == 0) {
			timeReinforcedJump = Time.time + 10;
		}
		if(playerStat == 2 && ammo < 10) {
			ammo = 10;
		}
		if(playerStat == 3 && invulnerability == 0) {
			Physics2D.IgnoreLayerCollision(8,9, true);
			halo.enabled = true;
			invulnerability = Time.time + 10;
		}
	}
}
