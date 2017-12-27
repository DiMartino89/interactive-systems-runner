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
	private SpriteRenderer rend;
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
	
	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
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
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isDead == false && isFinished == false) {
			if(Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D))) {
				transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
			}
			
			if(Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A))) {
				transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
			}
			
			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				if(timeReinforcedJump < Time.time && grounded == true) {
					player.velocity = new Vector2(0, jumpHeight);
				} else if(timeReinforcedJump > Time.time && grounded == true) {
					player.velocity = new Vector2(0, jumpHeight * 1.2f);
				}
			}
			
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
			
			float move = Input.GetAxis("Horizontal");
			
			if(move < 0) {
				isFlipped = true;
			} else if(move > 0) {
				isFlipped = false;
			}
			
			flip();
		}
		
		if(timeReinforcedJump < Time.time) {
			timeReinforcedJump = 0;
			currJumping.text = "Jump: " + 0 + "s";
		}
		
		if(invulnerability < Time.time) {
			invulnerability = 0;
			currInvulnerability.text = "Inv: " + 0 + "s";
			Physics2D.IgnoreLayerCollision(8,9, false);
			halo.enabled = false;
		}
		
		if(isDead == true && lifes > 0 && timeToRespawn < Time.time) {
			transform.position = lastCheckpoint;
			rend.enabled = true;
			Physics2D.IgnoreLayerCollision(8,9, false);
			isDead = false;
			isTriggered = false;
		}
		
		if(lifes == 0) {	
			Physics2D.IgnoreLayerCollision(8,9, true);
			gameOver.SetActive(true);
		}
		
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
				isTriggered = true;
				grounded = false;
			}
		}
		
		if(col.tag == "Checkpoint") {
			lastCheckpoint = transform.position;
			grounded = false;
		}
		
		if(col.tag == "FallDownCatch") {
			if(isTriggered == false) { 
				lifes--;
				Physics2D.IgnoreLayerCollision(8,9, false);
				if(lifes > 0) {
					timeToRespawn = Time.time + 2;
				}
				isDead = true;
				rend.enabled = false;
				isTriggered = true;
				grounded = false;
			}
		}
		
		if (col.tag == "Finish") {			
			winGame.SetActive(true);
			isFinished = true;
			rend.enabled = false;
			Physics2D.IgnoreLayerCollision(8,9, false);
		}
	}
	
	void OnTriggerStay2D(Collider2D col) {
		grounded = true;    
	}

	void OnTriggerExit2D(Collider2D col) {
		grounded = false;
	}
	
	void fire() {
		bulletPos = transform.position;
		if(isFlipped == false) {
			bulletPos.x += 1.0f;
		} else {
			bulletPos.x -= 1.0f;
		}
		clone = Instantiate(bullet, bulletPos, Quaternion.identity) as GameObject;
	}
	
	void flip() {
		if(isFlipped == false) {
			rend.flipX = false;
		} else {
			rend.flipX = true;
		}
	}
	
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
