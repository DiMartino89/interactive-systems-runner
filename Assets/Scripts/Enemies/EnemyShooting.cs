using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

	public float speed = 3.0f;
	public float rotation = 3.0f;
	public float fireRate = 3.0f;
	public float nextFire = 5.0f;
	public GameObject bullet;
	
	public AudioClip shot;

	private Vector3 targetPos;
	private Vector2 startPos;
	private GameObject clone;
	private AudioSource source;
	 
    void Start() {
		startPos = transform.position;
		source = GetComponent<AudioSource>();
		Physics2D.IgnoreLayerCollision(9,9, true);
    }
     
    void FixedUpdate() {
        if(Time.time > nextFire) {
			nextFire = nextFire + fireRate;
			targetPos = GameObject.FindWithTag("Player").transform.position;
			fire();
		}
		
		if(clone != null) {		
			clone.GetComponent<Rigidbody2D>().velocity = clone.transform.forward * speed * 10f;
		}
    }
	
	void fire() {	
		Destroy(clone);
	
		clone = Instantiate(bullet, startPos, Quaternion.identity) as GameObject;
		
		float distanceToTarget = Vector2.Distance(startPos, targetPos);
		
		source.volume = 1/distanceToTarget;
		source.PlayOneShot(shot);
		
		clone.transform.rotation = Quaternion.Slerp(clone.transform.rotation, Quaternion.LookRotation(targetPos - clone.transform.position), rotation * Time.deltaTime);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerBullet") {
			gameObject.SetActive(false);
		}
	}
}
