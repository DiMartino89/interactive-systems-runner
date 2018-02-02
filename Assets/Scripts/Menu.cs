using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

public Text difficultyLabel;
public Text levelLengthLabel;
public Slider difficultySlider;
public Slider levelLengthSlider;
public Toggle soundToggle;
public bool soundOn = true;

public GameObject levelGen;
	
	void Start() {
		//soundToggle = soundToggle.GetComponent<Toggle>();
	}
	
	// Within Main-menu
	
	// Load Game-Scene
    public void startGame(string sceneName) {
		Application.LoadLevel(sceneName);
	}
	
	// Load Editor-Scene
	public void openEditor(string sceneName) {
		Application.LoadLevel(sceneName);
	}
	
	// Load Setting-Scene
	public void openSettings(string sceneName) {
		Application.LoadLevel(sceneName);
	}
	
	// Quit Game
	public void quitGame() {
		Application.Quit();
	}
	
	// Within Editor and settings
	
	// Change the difficulty label and value within settings
	public void selectDifficulty() {
		int value =  (int)difficultySlider.value;
		if(value == 1) {
			difficultyLabel.text = "Difficulty: Easy"; 
		}
		if(value == 2) {
			difficultyLabel.text = "Difficulty: Medium"; 
		}
		if(value == 3) {
			difficultyLabel.text = "Difficulty: Hard"; 
		}
		
		levelGen.GetComponent<LevelGeneration>().changeDifficulty(value);
	}
	
	// Turn on/off Sound within settings
	public void switchSound() 
    {
		if(!AudioListener.pause) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}
    }
	
	// Change the Level-length label and value within settings
	public void selectLevelLength() {
		int value = (int)levelLengthSlider.value;
		levelLengthLabel.text = "Level-Length: " + value;
		levelGen.GetComponent<LevelGeneration>().changeLevelLength(value);
	}
	
	// Go back to Menu from Settings or Game
	public void backToMenu(string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
