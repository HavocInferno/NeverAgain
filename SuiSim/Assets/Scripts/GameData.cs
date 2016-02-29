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
    private float HP;
    public float health
    {
        get { return HP; }
        set { HP = value; }
    }

    private int Score;
    public int score
    {
        get { return Score; }
        set { Score = value; }
    }

    private int okM;
    public int overkillMulti
    {
        get { return okM; }
        set { okM = value; }
    }

    private int Overdose;
    public int overdose
    {
        get { return Overdose; }
        set { Overdose = value; }
    }
}
