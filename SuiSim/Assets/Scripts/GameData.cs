using UnityEngine;
using System.Collections;

public class GameData {

    private static GameData instance;

    private GameData()
    {
        if (instance != null)
            return;

        instance = this;
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
        set { okM = value; GameUI.UIes.Multi = okM*odM; }
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

    public class highscoreEntry
    {
        public string name = "";
        public int score = 0;
    }
    private highscoreEntry[] Highscores = new highscoreEntry[5];
    public highscoreEntry[] highscores
    {
        get { return Highscores; }
        set { Highscores = value; }
    }
}
