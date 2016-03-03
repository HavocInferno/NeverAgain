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
    public Transform hudPause;
    public Transform hudGame;
    public Transform menuPause;
    public Transform menuGame;
    public Transform scoreActive;
    public Transform scoreInactive;
    public Image overdoseBlood;
    public static GameUI UIes;
    private int multi = 1;
    private int overdose;
    public float switchspeed;
    // Use this for initialization
    void Start () {
        imSpiel = false;
        OverdoseBool = false;
        textsize = score1.fontSize;
       // StartCoroutine(testicle());
       // test();    
        UIes = this;
    }

	
	// Update is called once per frame
	void Update () {
        score1.fontSize = (int) Mathf.Lerp(score1.fontSize, textsize, Time.deltaTime * 10);
        score2.fontSize = score1.fontSize;
        overdose1.fontSize = (int) (40 * ((Mathf.Sin(Time.realtimeSinceStartup * 6) / 6.0f) + 1));
        overdose2.fontSize = overdose1.fontSize;
        multi1.fontSize = multi2.fontSize = (int)(30 * ((Mathf.Sin(Time.realtimeSinceStartup * 20) / 6.0f) + 1.0f));

        // Anzeige des menüs oder huds
        if (!imSpiel)
        {
            hud.transform.position = Vector3.Lerp(hud.transform.position, hudPause.position, Time.deltaTime* switchspeed);
            menu.transform.position = Vector3.Lerp(menu.transform.position, menuPause.position, Time.deltaTime * switchspeed);
            menu.transform.rotation = Quaternion.Slerp(menu.transform.rotation, menuPause.rotation, Time.deltaTime * switchspeed);
            scoreboard.transform.position = Vector3.Lerp(scoreboard.transform.position, scoreActive.position, Time.deltaTime * switchspeed);
        }
        if(imSpiel)
        {
            hud.transform.position = Vector3.Lerp(hud.transform.position, hudGame.position, Time.deltaTime * switchspeed);
            menu.transform.rotation = Quaternion.Slerp(menu.transform.rotation, menuGame.rotation, Time.deltaTime * switchspeed);
            menu.transform.position = Vector3.Lerp(menu.transform.position, menuGame.position, Time.deltaTime * switchspeed);
            scoreboard.transform.position = Vector3.Lerp(scoreboard.transform.position, scoreActive.position, Time.deltaTime * switchspeed);
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
    public void setHighscore(HighscoreEntry[] entries)
    {
        for(int i = 0; i < 5; i++)
        {
            Highscores[i].text = (i + 1) + ". " + entries[i].name + "\t" + entries[i].score;
        }
    }

    public class HighscoreEntry
    {
        public string name = "";
        public int score = 0;
    }

    public void test()
    {
        HighscoreEntry[] highi = new HighscoreEntry[5];
        for(int i = 0; i< 5; i++)
        {
            HighscoreEntry highE = new HighscoreEntry();
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

    #region lesbuttones
    public void buttonstart()
    {
        imSpiel = true;
        GameData.Instance.spawnPlayer();
    }
    #endregion
}
