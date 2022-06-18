using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    
    [Header("Set in inspector")]
    public Text scoreValue;
    public static readonly int numberOfTries = 5;
    
    [Header("Set dynamically")]
    public static int[] scoreOnLevels = new int[MissionDemolition.S.levelMax];
    public static int score = 0;
    

    public static void AddScore()
    {
        score += MissionDemolition.S.shotsTaken;
        PlayerPrefs.SetInt($"Level{MissionDemolition.S.level}", score);
        scoreOnLevels[MissionDemolition.S.level] = PlayerPrefs.GetInt($"Level{MissionDemolition.S.level}");
        score = 0;
    }

    public void ShowScore()
    {
        for (int i = 0; i < scoreOnLevels.Length; i++)
        {
            scoreValue.text += $"On level {i+1} it took you {scoreOnLevels[i]} tries\n";
        }
        int sumScore = 0;
        foreach (var score in scoreOnLevels)
        {
            sumScore += score;
        }
        scoreValue.text += $"It took you {sumScore} tries to beat the game!";
        return;
    }
  

    private void Awake()
    {
        ShowScore();
    }
}
   
