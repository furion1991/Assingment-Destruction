using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MySceneControl : MonoBehaviour
{
    [Header("Set in inspector")]
    public Button resetScore;
    public Button restartGame;
    public Button exitGame;

    
    private void Awake()
    {
        resetScore.onClick.AddListener(ResetScore);
        restartGame.onClick.AddListener(StartNewGame);
        exitGame.onClick.AddListener(ExitGame);
    }

    public void ResetScore()
    {
        HighScore.scoreOnLevels = new int[MissionDemolition.S.levelMax];
        HighScore.score = 0;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
