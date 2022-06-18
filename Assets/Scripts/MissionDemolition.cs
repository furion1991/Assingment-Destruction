using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static public MissionDemolition S;
    
    [Header("Set in Inspector")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamycally")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show SlingShot";

    void Start()
    {
        S = this;

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        }

        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Both");
        

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
        ProjectLine.S.Clear();

    }

    void UpdateGUI()
    {
        uitLevel.text = $"Level: {level + 1} of {levelMax}";
        uitShots.text = $"Shots taken: {shotsTaken}";
    }

    void Update()
    {
        UpdateGUI();

        if (S.shotsTaken > HighScore.numberOfTries)
        {
            HighScore.score += S.shotsTaken;
            StartLevel();
        }

        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            HighScore.AddScore();
            Invoke("NextLevel", 2f);
            
        }
        
        //Debug.Log($"Game mode is {mode}");
        //Debug.Log($"Goal is {Goal.goalMet}");
    }


    public void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            SceneManager.LoadScene(1);
        } 
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if(eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;

        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}

