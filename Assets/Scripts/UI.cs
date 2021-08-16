using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public Canvas tutorialCanvas;
    public Canvas ingameMenuCanvas;
    public Canvas fireCanvas;
    public Canvas roundScoreCanvas;
    public Canvas endScreenCanvas;
    public List<Canvas> canvasList;
    public bool menuOpen = false;

    private int activeCanvasIndex;
    private float scoringTimer;
    
	// Use this for initialization
	void Start () {
        fireCanvas.enabled = true;
        roundScoreCanvas.enabled = false;
        endScreenCanvas.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateChargePower(float chargeAmount)
    {
        fireCanvas.GetComponent<UIFire>().UpdateChargePower(chargeAmount);
    }

    public void UpdateProjectilePosition(float height, float distance)
    {
        fireCanvas.GetComponent<UIFire>().UpdateProjectilePosition(height, distance);
    }

    public void UpdateTotalScore(int score)
    {
        fireCanvas.GetComponent<UIFire>().UpdateScore(score);
    }

    public void UpdateRoundScore(RoundScores roundScores)
    {
        canvasList[activeCanvasIndex].enabled = false;
        roundScoreCanvas.enabled = true;
        roundScoreCanvas.GetComponent<UIRoundScore>().UpdateRoundScore(roundScores);
    }

    public void UpdateActiveCanvas(int index)
    {
        roundScoreCanvas.enabled = false;
        activeCanvasIndex = index;
        foreach (Canvas c in canvasList)
        {
            c.enabled = false;
        }
        canvasList[index].enabled = true;
    }

    public void UpdateProjectilesLeft(int projectilesLeft)
    {
        fireCanvas.GetComponent<UIFire>().UpdateProjectilesLeft(projectilesLeft);
    }

    public void ShowTutorialScreen()
    {
        fireCanvas.enabled = false;
        tutorialCanvas.enabled = true;
    }

    public void HideTutorialScreen()
    {
        fireCanvas.enabled = true;
        tutorialCanvas.enabled = false;
    }

    public void ShowEndScreen(int totalScore, int currentHighscore, string currentHighscoreName, bool newHighscore)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        endScreenCanvas.enabled = true;
        fireCanvas.enabled = false;
        endScreenCanvas.GetComponent<UIEndScreen>().LoadEndScreen(totalScore,currentHighscore,currentHighscoreName,newHighscore);
    }

    public void ToggleIngameMenu()
    {
        if (menuOpen)
        {
            ingameMenuCanvas.enabled = false;
            menuOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        } else {
            ingameMenuCanvas.enabled = true;
            menuOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0f;
        }
    }

    public void UpdateScoringStarted()
    {
        scoringTimer = 0f;
        StartCoroutine(ScoringTimer());
    }

    IEnumerator ScoringTimer()
    {

        while (scoringTimer < 4.2f)
        {
            scoringTimer += Time.deltaTime;
            fireCanvas.GetComponent<UIFire>().UpdateCurrentlyScoring((int)scoringTimer);
            yield return null;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
