using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlacement : MonoBehaviour {
	
	public GameObject enemyRolling, enemyJumping, enemyFollowing, enemyShooting;
	
	public void PlaceEnemies(Vector2 coords) {
		float rand = Random.value;
		
		if(rand <= 0.3f) {
			Instantiate(enemyRolling, new Vector2(coords.x, coords.y + 1), Quaternion.identity);
		}
		if(rand > 0.3f && rand < 0.6f) {
			Instantiate(enemyJumping, new Vector2(coords.x, coords.y + 1), Quaternion.identity);
		}
		if(rand > 0.6f && rand < 0.7f) {
			Instantiate(enemyFollowing, new Vector2(coords.x, coords.y + 1), Quaternion.identity);
		}
		if(rand >= 0.7f) {
			Instantiate(enemyShooting, new Vector2(coords.x, coords.y + 1), Quaternion.identity);
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
