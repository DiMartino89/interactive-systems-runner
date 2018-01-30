using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

	public GameObject player;
	GameObject standardLook;
	GameObject newLook;
	
	public GameObject ground;
	public GameObject bridge;
	public GameObject gap;
	public GameObject finish;
	public GameObject checkpoint;
	public GameObject fallDownCatch;
	
	public int minGroundSize = 1;
	public int maxGroundSize = 10;	
	public int maxHazardSize = 4;
	public int maxHeight = 3;
	public int maxDrop = -3;
	
	static public int plattforms = 100;
	[Range (0.0f, 1f)] 
	public float hazardChance = .5f;
	[Range (0.0f, 1f)] 
	public float bridgeChance = .3f;
	
	static public int difficulty = 1;
	
	private int amountCheckpoints;
	private int nextCheckpoint;
	private int blockNum = 1;
	private int blockHeight; 
	private bool isHazard;
	
	void Awake() {
		DontDestroyOnLoad(this);
	}
	
	// Use this for initialization
	void Start () {
		// Load the edited PlayerLook from Folder
		try{
			newLook = Resources.Load("EditedAppearance/editedLook") as GameObject;
		}            
		catch(UnityException e){            
			Debug.Log(e);
		}
		// Check for new Look, if not exists, load the standard-Look
		if(newLook != null) {
			GameObject eLook = Instantiate(newLook, new Vector2(0,1.3f), Quaternion.identity) as GameObject;
			eLook.transform.SetParent(player.transform);
			Animator head = eLook.GetComponent<Animator>();
			Animator body = eLook.transform.Find("UpperBody").GetComponent<Animator>();
			Animator legs = eLook.transform.Find("UpperBody").transform.Find("LowerBody").GetComponent<Animator>();
			player.GetComponent<PlayerController>().assignAnimators(head, body, legs);
		} else {
			standardLook = Resources.Load("StandardAppearance/standardLook") as GameObject;
			GameObject sLook = Instantiate(standardLook, new Vector2(0,1.3f), Quaternion.identity) as GameObject;
			sLook.transform.SetParent(player.transform);
			Animator head = sLook.GetComponent<Animator>();
			Animator body = sLook.transform.Find("UpperBody").GetComponent<Animator>();
			Animator legs = sLook.transform.Find("UpperBody").transform.Find("LowerBody").GetComponent<Animator>();
			player.GetComponent<PlayerController>().assignAnimators(head, body, legs);
		}
		
		// Instantiate first tile for the player
		Instantiate(ground, new Vector2(0,0), Quaternion.identity);
		
		// Instantiate the 5 FallDownCatches before the first tile as first tile is not directly on the left border
		for(int i = -5; i < 1; i++) {
			Instantiate(fallDownCatch, new Vector2(i, blockHeight - 10), Quaternion.identity);
		}
		
		// Calculate the amount of checkpoints depending on the level-length and set first position
		amountCheckpoints = Mathf.RoundToInt((plattforms / 100) * 5);
		if(amountCheckpoints > 0) {
			nextCheckpoint = plattforms / amountCheckpoints;
		}
		
		// Loop through all plattforms
		for(int plat = 1; plat < plattforms; plat++) {
			
			// Check for if there is a gap next, 50% Chance
			if(isHazard) {
				isHazard = false;
			} else {
				if(Random.value < hazardChance) {
					isHazard = true;
				} else {
					isHazard = false;
				}
			}
			
			// Insert the gap and a FallDownCatch below, but only when it is not the end of the level
			if(isHazard && plat != (plattforms) && plat != (plattforms - 1)) {
				int hazardSize = Mathf.RoundToInt(Random.Range(1, maxHazardSize));
				for(int hazard = 0; hazard < hazardSize; hazard ++) {
					Instantiate(fallDownCatch, new Vector2(blockNum + hazard, blockHeight - 10), Quaternion.identity);
				}
				blockNum += hazardSize;
			} else {	
				
				// If there could be a bridge next
				if(Random.value < bridgeChance) {
					int platSize = Mathf.RoundToInt(Random.Range(minGroundSize, maxGroundSize));
				
					blockHeight = blockHeight + Random.Range(maxDrop, maxHeight);
					
					// After setting the size out of min and max, loop through it and set ground tile left and right from the bridge
					for(int tiles = 0; tiles < platSize; tiles ++) {
						if(tiles == 0 || tiles == (platSize - 1)) {
							Instantiate(ground, new Vector2(blockNum,blockHeight), Quaternion.identity);
							Instantiate(fallDownCatch, new Vector2(blockNum, blockHeight - 10), Quaternion.identity);
							blockNum++;
						} else {
							Instantiate(bridge, new Vector2(blockNum,blockHeight), Quaternion.identity);
							Instantiate(fallDownCatch, new Vector2(blockNum, blockHeight - 10), Quaternion.identity);
							blockNum++;
						}
						// If there is a bridge at the level-end, set the finish
						if(plat == (plattforms - 1) && tiles == (platSize - 1)) {
							Debug.Log("Finish");
							Instantiate(finish, new Vector2(blockNum, blockHeight + 1.3f), Quaternion.identity);
						}
					}
				} else {
				
					int platSize = Mathf.RoundToInt(Random.Range(minGroundSize, maxGroundSize));
					
					blockHeight = blockHeight + Random.Range(maxDrop, maxHeight);
					
					// If no bridge, loop through ground-tiles-size and set ground and FallDownCatch
					for(int tiles = 0; tiles < platSize; tiles ++) {
						Instantiate(ground, new Vector2(blockNum, blockHeight), Quaternion.identity);
						Instantiate(fallDownCatch, new Vector2(blockNum, blockHeight - 10), Quaternion.identity);
						
						// Depending on the difficulty, the chance of enemies and Items variates
						if(difficulty == 1) {
							if(Random.Range(0, 12) == 6) {
								GetComponent<EnemyPlacement>().PlaceEnemies(new Vector2(blockNum, blockHeight));
							}
							if(Random.Range(0, 28) == 14) {
								GetComponent<ItemPlacement>().PlaceItems(new Vector2(blockNum, blockHeight));
							}
						}
						if(difficulty == 2) {
							if(Random.Range(0, 8) == 4) {
								GetComponent<EnemyPlacement>().PlaceEnemies(new Vector2(blockNum, blockHeight));
							}
							if(Random.Range(0, 34) == 17) {
								GetComponent<ItemPlacement>().PlaceItems(new Vector2(blockNum, blockHeight));
							}
						}
						if(difficulty == 3) {
							if(Random.Range(0, 4) == 2) {
								GetComponent<EnemyPlacement>().PlaceEnemies(new Vector2(blockNum, blockHeight));
							}
							if(Random.Range(0, 40) == 20) {
								GetComponent<ItemPlacement>().PlaceItems(new Vector2(blockNum, blockHeight));
							}
						}
						// If plat equals the position of nextCheckpoint, set the checkpoint
						if(plat == nextCheckpoint) {
							Instantiate(checkpoint, new Vector2(blockNum, blockHeight + 1), Quaternion.identity);
							nextCheckpoint += (plattforms / amountCheckpoints);
						}
						// If plat is at the level-end, set the finish
						if(plat == (plattforms - 1) && tiles == (platSize - 1)) {
							Debug.Log("Finish");
							Instantiate(finish, new Vector2(blockNum, blockHeight + 1.3f), Quaternion.identity);
						}
						
						blockNum++;	
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Public-Function for adjust Difficulty from settings
	public void changeDifficulty(int value) {
		difficulty = value;
	}
	
	// Public-Function for adjust Level-length from settings
	public void changeLevelLength(int value) {
		plattforms = value;
	}
}
