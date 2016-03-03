﻿using UnityEngine;
using System.Collections;

public class GameData: MonoBehaviour {

    private static GameData instance;
    public GameObject playerPrefab;
    public Transform spawnPosition;
    public enum GameState { MainMenu, Playing, Stats, Won, Lost };
    [SerializeField]
    private GameObject playerMuppet;
    private GameState State;
    private bool first = true;
    [SerializeField]
    private GameObject firstplayer;
    public GameState state
    {
        get { return State; }
        set {
            Debug.Log(UnityEngine.StackTraceUtility.ExtractStackTrace() + " "+ value.ToString());
            if (State == GameState.Playing)
                playerMuppet.SetActive(true);
            State = value;
            if (State == GameState.Playing)
                playerMuppet.SetActive(false);
        }
    }
    public ArrayList choppaz = new ArrayList();
    public float StartDelay;

    private GameData()
    {
        if (instance != null)
            return;

        instance = this;
        state = GameState.MainMenu;
    }

    public static GameData Instance
    {
        get
        {
            if (instance == null)
                new GameData();

            return instance;
        }
    }

    //player
    private bool Dead;
    public bool dead
    {
        get { return Dead; }
        set { Dead = value; if (Dead) { health = 0f; } }
    }

    private float HP;
    public float health
    {
        get { return HP; }
        set { HP = value; while (HP <= 0 && !Dead) { HP += 100.0f; overkillMulti++; }; GameUI.UIes.health = (int)(HP * 10.0f); }
    }

    private int Score;
    public int score
    {
        get { return Score; }
        set { Score = Mathf.Max(value,0); GameUI.UIes.Score = Score; }
    }

    private int okM=1;
    private int odM=1;
    public int overkillMulti
    {
        get { return okM; }
        set { okM = value; 
            GameUI.UIes.Multi = okM*odM;
            if (value > 1)
            {
                GameUI.UIes.OverKillText.transform.localScale = new Vector3(2, 2, 2);
                GameUI.UIes.OverKillText.SetActive(true);
            }
        }
    }
    public int Multi
    {
        get { return okM * odM; }
    }
    public int overDoseMulti
    {
        get { return odM; }
        set { odM = value; GameUI.UIes.Multi = okM*odM; }
    }

    private int Overdose;
    public int overdose
    {
        get { return Overdose; }
        set { Overdose = value; GameUI.UIes.Overdose = Overdose; }
    }

    public class HighscoreEntry
    {
        public string name = "";
        public int score = 0;
    }
    private HighscoreEntry[] Highscores = new HighscoreEntry[5];
    public HighscoreEntry[] highscores
    {
        get { return Highscores; }
        set { Highscores = value; }
    }

    public void spawnPlayer()
    {
        if (first)
        {
            first = false;
            firstplayer.SetActive(true);
        }
        else
        {
            Destroy(Character.playerInstance);
            Instantiate(playerPrefab, spawnPosition.position, spawnPosition.rotation);
        }
        reset();
    }
    public void reset()
    {
        state = GameState.Playing;
        dead = false;
        overkillMulti = 1;
        score = 0;
        health = 100.0f;
        overDoseMulti = 1;
        overkillMulti = 1;
        overdose = 0;
        Collectible_Spawn.i.reset();
        foreach (Object i in choppaz)
            ((Helicock)i).reset();

    }
    public void showScoreboard()
    {
        state = GameState.Stats;
    }
    public void endGame()
    {
        //EndGame;
    }
    public void winLose()
    {
        if (score > 0)
            state = GameState.Won;
        else
            state = GameState.Lost;
    }
    public void leaveScoreBoard()
    {
        state = GameState.MainMenu;
    }
}
