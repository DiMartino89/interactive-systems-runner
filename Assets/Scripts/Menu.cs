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
    public void startGame(string sceneName) {
		Application.LoadLevel(sceneName);
	}
	
	public void openEditor(string sceneName) {
		Application.LoadLevel(sceneName);
	}
	
	public void openSettings(string sceneName) {
		Application.LoadLevel(sceneName);
	}
	
	public void quitGame() {
		Application.Quit();
	}
	
	// Within Editor and Settings
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
	
	public void switchSound() 
    {
		if(!AudioListener.pause) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}
    }
	
	public void selectLevelLength() {
		int value = (int)levelLengthSlider.value;
		levelLengthLabel.text = "Level-Length: " + value;
		levelGen.GetComponent<LevelGeneration>().changeLevelLength(value);
	}
	
	public void backToMenu(string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
