using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour {
	
	public GameObject itemAmmo, itemJump, itemInvulnerability;
	
	public void PlaceItems(Vector2 coords) {
		float rand = Random.value;
		
		// If random-value equals to one of the cases, place the appropriated Item +1 on y-axis to the current tile
		if(rand < 0.3f) {
			Instantiate(itemAmmo, new Vector2(coords.x, coords.y + 1), Quaternion.identity);
		}
		if(rand >= 0.3f && rand < 0.6f) {
			Instantiate(itemJump, new Vector2(coords.x, coords.y + 1), Quaternion.identity);
		}
		if(rand >= 0.6f && rand < 0.9f) {
			Instantiate(itemInvulnerability, new Vector2(coords.x, coords.y + 1), Quaternion.identity);
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
