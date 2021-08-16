using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoundScore : MonoBehaviour {
    public Text destroyedText;
    public Text displacedText;
    public Text heightText;
    public Text distanceText;
    public Text totalRoundScoreText;
    // Use this for initialization

    public void UpdateRoundScore(RoundScores roundScore)
    {
        destroyedText.text = roundScore.brokenJointsScore.ToString();
        displacedText.text = roundScore.ragdollDisplacementScore.ToString();
        heightText.text = roundScore.heightMultiplier.ToString();
        distanceText.text = roundScore.distanceMultiplier.ToString();
        totalRoundScoreText.text = roundScore.totalRoundScore.ToString();

    }
	
}
