using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundScores {

    public int brokenJointsScore;
    public int ragdollDisplacementScore;
    public float heightMultiplier;
    public float distanceMultiplier;
    public int totalRoundScore;


    // Use this for initialization
    public RoundScores(int brokenJoints, float ragdollDistanceSum, float projectileHeight, float projectileDistance) {
        CalculateRoundScore(brokenJoints, ragdollDistanceSum, projectileHeight, projectileDistance);

    }

    private void CalculateRoundScore(int brokenJoints, float ragdollDistanceSum, float projectileHeight, float projectileDistance)
    {
        
        brokenJointsScore = brokenJoints * 2;
        ragdollDisplacementScore = (int)(ragdollDistanceSum * 2);
        heightMultiplier = 1 + (Mathf.Round(projectileHeight / 10) / 10);
        distanceMultiplier = 1 + (Mathf.Round(projectileDistance / 10) / 10);
        totalRoundScore = (int)((brokenJointsScore + ragdollDisplacementScore) * heightMultiplier * distanceMultiplier);
    }

	
}
