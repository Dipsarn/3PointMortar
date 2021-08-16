using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIEndScreen : MonoBehaviour {

    public Canvas currentHighscoreCanvas;
    public Canvas newHighscoreCanvas;
    public Text totalScoreText;
    public Text currentHighscoreValueText;
    public Text currentHighscoreNameText;
    public InputField nameInput;
    public Button saveHighscoreNameButton;

    private int totalScore;
	// Use this for initialization
	void Start () {
        currentHighscoreCanvas.enabled = false;
        newHighscoreCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadEndScreen(int totalScore, int currentHighscore, string currentHighscoreName, bool newHighscore)
    {
        this.totalScore = totalScore;
        totalScoreText.text = totalScore.ToString();
        if (newHighscore)
        {
            newHighscoreCanvas.enabled = true;
            
        } else
        {
            currentHighscoreCanvas.enabled = true;
            currentHighscoreValueText.text = currentHighscore.ToString();
            currentHighscoreNameText.text = currentHighscoreName;
        }
    }

    public void SaveNewHighscore()
    {
        PlayerPrefs.SetString("HighscoreName", nameInput.text);
        PlayerPrefs.SetInt("Highscore", totalScore);
        saveHighscoreNameButton.enabled = false;
        nameInput.interactable = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
