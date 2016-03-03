using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    public Text score1, score2, overdose1, overdose2, multi1, multi2;
    public Text[] Highscores;
    private int textsize;
    private int score;
    public int health;
    public Slider slides;
    private bool imSpiel, overdoseBool;
    public GameObject hud;
    public GameObject menu;
    public GameObject scoreboard;
    public GameObject win, lose;
    public Transform hudInactive;
    public Transform hudActive;
    public Transform menuActive;
    public Transform menuInactive;
    public Transform scoreActive;
    public Transform scoreInactive;
    public Transform winActive, winInactive;
    public Image overdoseBlood;
    public static GameUI UIes;
    private int multi = 1;
    private int overdose;
    public float switchspeed;
    public InputField winName, loseName;
    public GameObject OverKillText;
    // Use this for initialization
    void Awake()
    {

        UIes = this;
        imSpiel = true;
        OverdoseBool = false;
        textsize = score1.fontSize;
        // StartCoroutine(testicle());
        //test();
        UIes = this;
    }


	
	// Update is called once per frame
	void Update () {
        OverKillText.transform.localScale = Vector3.Lerp(OverKillText.transform.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime*2);
        if (OverKillText.transform.localScale.x <= 1)
            OverKillText.SetActive(false);
        score1.fontSize = (int) Mathf.Lerp(score1.fontSize, textsize, Time.deltaTime * 10);
        score2.fontSize = score1.fontSize;
        overdose1.fontSize = (int) (40 * ((Mathf.Sin(Time.realtimeSinceStartup * 6) / 6.0f) + 1));
        overdose2.fontSize = overdose1.fontSize;
        multi1.fontSize = multi2.fontSize = (int)(30 * ((Mathf.Sin(Time.realtimeSinceStartup * 20) / 6.0f) + 1.0f));


        // Anzeige des menüs oder huds
        switch (GameData.Instance.state)
        {
            case GameData.GameState.Playing:
                moveTo(scoreboard, scoreInactive);
                moveTo(win, winInactive);
                moveTo(lose, winInactive);
                if (!imSpiel)
                {
                    moveTo(hud, hudInactive);
                    moveTo(menu, menuActive);

                }
                else
                {
                    moveTo(hud, hudActive);
                    moveTo(menu, menuInactive);
                }
                break;

            case GameData.GameState.MainMenu:
                moveTo(scoreboard, scoreInactive);
                moveTo(win, winInactive);
                moveTo(lose, winInactive);
                moveTo(hud, hudInactive);
                moveTo(menu, menuActive);
                break;

            case GameData.GameState.Stats:
                moveTo(scoreboard, scoreActive);
                moveTo(win, winInactive);
                moveTo(lose, winInactive);
                moveTo(hud, hudInactive);
                moveTo(menu, menuInactive);
                break;

            case GameData.GameState.Won:
                moveTo(scoreboard, scoreInactive);
                moveTo(win, winActive);
                moveTo(lose, winInactive);
                moveTo(hud, hudInactive);
                moveTo(menu, menuInactive);
                break;

            case GameData.GameState.Lost:
                moveTo(scoreboard, scoreInactive);
                moveTo(win, winInactive);
                moveTo(lose, winActive);
                moveTo(hud, hudInactive);
                moveTo(menu, menuInactive);
                break;
            default:
                break;
        }
        if (Input.GetButtonDown("Escape"))
            imSpiel = !imSpiel;


    // Lebensbalken
    slides.value = health;

        // Overdose Anzeige
    }

    // Testing
    IEnumerator testicle()
    {
        while (true)
        {
            Score += 100;
            health -= 1;
            yield return new WaitForSeconds(1);
        }
    }

    public void moveTo(GameObject obj, Transform trans)
    {
        obj.transform.position = Vector3.Lerp(obj.transform.position, trans.position, Time.deltaTime * switchspeed);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, trans.rotation, Time.deltaTime * switchspeed);
        obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, trans.localScale, Time.deltaTime * switchspeed);
    }

    // Zum aktualisieren des Scores
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            score1.fontSize = textsize + 10;
            score1.text = "Score   " + score;
            score2.text = score1.text;
        }
    }
    public int Multi
    {
        get
        {
            return multi;
        }
        set
        {
            multi = value;
            if (multi > 1)
            {
                multi1.text = "x" + multi;
                multi2.text = multi1.text;
                multi1.enabled = multi2.enabled = true;
            }
            else
                multi1.enabled = multi2.enabled = false;
            /*score1.fontSize = textsize + 10;
            score1.text = "Score   " + score;
            score2.text = score1.text;*/
        }
    }
    public void setHighscore(GameData.HighscoreEntry[] entries)
    {
        for(int i = 0; i < 5; i++)
        {
            if (entries[i] != null)
            {
                Highscores[i].text = (i + 1) + ". " + entries[i].name + "\t" + entries[i].score;
            } else
            {
                Highscores[i].text = (i + 1) + ". " + "-" + "\t" + "-";
            }
        }
    }

 
    public void test()
    {
        GameData.HighscoreEntry[] highi = new GameData.HighscoreEntry[5];
        for(int i = 0; i< 5; i++)
        {
            GameData.HighscoreEntry highE = new GameData.HighscoreEntry();
            highE.name = "pu$$ysh4ft" + i;
            highE.score = 3587 + i;
            highi[i] = highE;
        }
        setHighscore(highi);
    }
    public int Overdose
    {
        get
        {
            return overdose;
        }
        set
        {
            overdose = value;
        }
    }
    public bool OverdoseBool {
        get { return overdoseBlood; }
        set { overdoseBool = value;
            overdose1.enabled = overdose2.enabled = overdoseBlood.enabled = overdoseBool;
        }
    }
    public void saveNameW()
    {
        GameData.HighscoreEntry newScore = new GameData.HighscoreEntry();
        newScore.name = winName.text;
        newScore.score = GameData.Instance.score;
        // Add Penis
        GameData.Instance.highscores = pushHighscore(GameData.Instance.highscores, newScore);
        setHighscore(GameData.Instance.highscores);

        Debug.Log("HighscoreEntry: " + newScore.name + " " + newScore.score);
        GameData.Instance.state = GameData.GameState.MainMenu;
    }
    public void saveNameL()
    {
        GameData.HighscoreEntry newScore = new GameData.HighscoreEntry();
        newScore.name = loseName.text;
        newScore.score = GameData.Instance.score;
        Debug.Log("HighscoreEntry: "+ newScore.name +" "+newScore.score);
        // Add Penis
        GameData.Instance.highscores = pushHighscore(GameData.Instance.highscores, newScore);
        setHighscore(GameData.Instance.highscores);

        GameData.Instance.state = GameData.GameState.MainMenu;
    }

    private void sortHighscores(GameData.HighscoreEntry[] input)
    {
        bool unsorted = true;

        while(unsorted)
        {
            unsorted = false;
            for (int i = 0; i < 4; i++)
            {
                if (input[i] != null)
                {
                    if (input[i + 1] != null)
                    {
                        if (input[i].score < input[i + 1].score)
                        {
                            unsorted = true;
                            GameData.HighscoreEntry tmpi = input[i];
                            input[i] = input[i + 1];
                            input[i + 1] = tmpi;
                        }
                    }
                } else
                {
                    if (input[i + 1] != null)
                    {
                        unsorted = true;
                        input[i] = input[i + 1];
                        input[i + 1] = null;
                    }
                }
            }
        }
    }

    private GameData.HighscoreEntry[] pushHighscore(GameData.HighscoreEntry[] input, GameData.HighscoreEntry newHS)
    {
        bool fits = false; 
        for(int i = 0; i < input.Length; i++)
        {
            if (input[i] != null)
            {
                if (input[i].score < newHS.score)
                    fits = true;
            } else
            {
                fits = true;
            }
        }

        if(fits)
        {
            sortHighscores(input);
            input[input.Length-1] = newHS;
            sortHighscores(input);
            return input;
        } else
        {
            sortHighscores(input);
            return input;
        }
    }

    #region lesbuttones
    public void buttonstart()
    {
        imSpiel = true;
        GameData.Instance.spawnPlayer();
    }
    #endregion
}
