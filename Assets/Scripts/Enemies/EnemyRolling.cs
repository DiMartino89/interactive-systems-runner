using UnityEngine;
using System.Collections;
 
public class EnemyRolling : MonoBehaviour
{

	private Rigidbody2D mainBody;
	public Transform leftEdge, rightEdge;
	//private Animator anim;

	public float moveSpeed = 0.02f;
	public float chaseSpeed = 0.10f;
	public float stopTime = 1;
	[Range (0.0f, 1f)]
	public float chaserChance = 0.5f;
	public float chaserDistance = 3;
	private float move, timer;

	private bool isStopped, isRight, isChasing, isChaser;

	private int maskLayer = 12;

	// Use this for initialization
	void Start() {
		mainBody = GetComponentInParent<Rigidbody2D>();
		if (Random.value < chaserChance) {
			isChaser = true;
		}
		
		Physics2D.IgnoreLayerCollision(9,9, true);
	}

	void FixedUpdate () {

		float distance = Vector2.Distance (transform.position, GameObject.Find ("Player").transform.position);
		Vector2 direction = transform.position - GameObject.Find("Player").transform.position;

		mainBody.MovePosition(mainBody.position + Vector2.left * move);

		if (isChaser == true && distance < chaserDistance) {
			if (direction.x > 0) {
				isRight = false;
				move = chaseSpeed;
			} else {
				isRight = true;
				move = -chaseSpeed;
			}
		} else {
			if (isStopped == true) {
				move = 0;
				timer += Time.deltaTime;
				if (timer >= stopTime) {
					isStopped = false;
					timer = 0;
				}
			} else {
				if (isRight == false) {
					move = moveSpeed;
				} else {
					move = -moveSpeed;
				}
			}
		}

		//edge detection raycasts
		RaycastHit2D hitLeft = Physics2D.Raycast(leftEdge.position, Vector2.down, 0.5f, 1 << maskLayer);
		RaycastHit2D hitRight = Physics2D.Raycast(rightEdge.position, Vector2.down, 0.5f, 1 << maskLayer);

		//if is going left check if the raycast no longer detects the ground
		if (isRight == false) {
			if (hitLeft.collider == null) {
				isStopped = true;
				move = 0;
				//if saw is a chaser, do not change the direction when chasing
				if (isChasing == false) {
					isRight = true;
				}
			}


		}

		//if is going right check if the raycast no longer detects the ground
		if (isRight == true) {
			if (hitRight.collider == null) {
				isStopped = true;
				move = 0;
				//if saw is a chaser, do not change the direction when chasing
				if (isChasing == false) {
					isRight = false;
				}
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerBullet") {
			gameObject.SetActive(false);
		}
	}
}