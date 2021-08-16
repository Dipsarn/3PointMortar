using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameEngine gameEngine;
    public UI ui;
    public GameObject mortarBundle;

    private Mortar mortarControl;
    private bool chargingFire = false;
    private bool roundComplete = true;
    private bool currentlyScoring = false;
    private bool gameEnded = false;
    private bool gameStarted = false;
    private bool roundScreenShowing = false;
 
    private float firePower = 0f;
    private int projectilesLeft = 5;


	// Use this for initialization
	void Start () {

        mortarControl = mortarBundle.GetComponent<Mortar>();
        ui.ShowTutorialScreen();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameStarted)
        {
            if (!gameEnded || ui.menuOpen)
            {
                CheckGameState();

                UpdateUIProjectilePosition(gameEngine.GetProjectileHeight(), gameEngine.GetProjectileDistance());
                UpdateUIProjectilesLeft();
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true;
                ui.HideTutorialScreen();
            }
        }
       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateUIToggleIngameMenu();
        }

        
        
    }

    private void CheckGameState()
    {

        // Game ended
        if (projectilesLeft == 0 && !roundScreenShowing)
        {
            gameEnded = true;
            UpdateUIShowEndScreen();
        }
        else
        {
            if (chargingFire)
            {
                ChargeFire();
            }

            if (roundScreenShowing)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    roundScreenShowing = false;
                    gameEngine.DeactivateScenicCamera();
                    ui.UpdateActiveCanvas(0);
                }
            }
            else
            {
                PlayerInput();
            }



            if (!roundComplete)
            {
                if (gameEngine.HasFinishedScoring())
                {
                    UpdateUIRoundScores(gameEngine.GetRoundScores());
                    UpdateUIScore(gameEngine.GetTotalScore());
                    gameEngine.ActivateScenicCamera();
                    roundComplete = true;
                    roundScreenShowing = true;
                    projectilesLeft--;
                    currentlyScoring = false;
                }
                if (gameEngine.IsCurrentlyScoring() && !currentlyScoring)
                {
                    ui.UpdateActiveCanvas(gameEngine.GetActiveCameraIndex());
                    currentlyScoring = true;
                    UpdateUIScoringStarted();
                }
            }
        }

      
        
    }


    // ----------- Controls -------------
    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            mortarControl.AimVertical(1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            mortarControl.AimVertical(-1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            mortarControl.AimHorizontal(-1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            mortarControl.AimHorizontal(1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && roundComplete)
        {

            chargingFire = true;

        }

        if (Input.GetKeyUp(KeyCode.Space) && chargingFire)
        {
            roundComplete = false;
            mortarControl.Fire(firePower);
            
            firePower = 0f;
            ui.UpdateChargePower(firePower);
            chargingFire = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameEngine.ToggleCamera();
            UpdateUIActiveCanvas(gameEngine.GetActiveCameraIndex());
        }

        if (Input.GetKeyDown(KeyCode.C) && !gameEngine.IsSlowMotion())
        {
            gameEngine.ActivateSlowMotion();
        }

        if (Input.GetKeyUp(KeyCode.C) && gameEngine.IsSlowMotion())
        {
            gameEngine.DeactivateSlowMotion();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            int spawnPoint = 0;
          
            bool result = int.TryParse(Input.inputString, out spawnPoint);
            if (result)
            {
                spawnPoint = Mathf.Clamp(spawnPoint, 1, 5);
                gameEngine.ChangeSpawnPoint(spawnPoint);
            }
        }


    }


    // ----------- Firing ------------------
    private void ChargeFire()
    {
        firePower += Time.deltaTime * 10f;
        firePower = Mathf.Clamp(firePower, 0, 50);
        ui.UpdateChargePower(firePower);
        
    }


    // --------------- UI Updates ------------
    private void UpdateUIScore(int totalScore)
    {
        ui.UpdateTotalScore(totalScore);
        
    }

    private void UpdateUIProjectilePosition(float height, float distance)
    {
        ui.UpdateProjectilePosition(height, distance);
    }

    private void UpdateUIActiveCanvas(int index)
    {
        ui.UpdateActiveCanvas(index);
    }

    private void UpdateUIRoundScores(RoundScores roundScores)
    {
        ui.UpdateRoundScore(roundScores);
    }

    private void UpdateUIProjectilesLeft()
    {
        ui.UpdateProjectilesLeft(projectilesLeft);
    }
   
    private void UpdateUIShowEndScreen()
    {
        ui.ShowEndScreen(gameEngine.GetTotalScore(),gameEngine.GetCurrentHighscore(),gameEngine.GetHighscoreName(),gameEngine.IsNewHighscore());
    }

    private void UpdateUIToggleIngameMenu()
    {
        ui.ToggleIngameMenu();
    }

    private void UpdateUIScoringStarted()
    {
        ui.UpdateScoringStarted();
    }
}
