using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFire : MonoBehaviour {

    public Image chargingImage;
    public Text scoreText;
    public Text projectileHeightText;
    public Text projectileDistanceText;
    public Text projectilesLeftText;

    // Use this for initialization
    void Start () {
        chargingImage.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateChargePower(float chargeAmount)
    {
        

        chargingImage.fillAmount = 0.68f * (chargeAmount / 50f) + 0.25f;
            
       
    }

    public void UpdateProjectilePosition(float height, float distance)
    {
        projectileHeightText.text = height.ToString("f0");
        projectileDistanceText.text = distance.ToString("f0");
    }

    public void UpdateProjectilesLeft(int projectilesLeft)
    {
        projectilesLeftText.text = projectilesLeft.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }


    public void UpdateCurrentlyScoring(int timeLeft)
    {
        scoreText.text = "Calculating: " + (5 - timeLeft).ToString();
    }

}
