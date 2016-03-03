using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {

    private Rigidbody rb;
    public Transform directions;
    public float thrust = 1.0f;

    public GameObject deathEffect;
    public GameObject hitEffect;
    public float maxVel = 35.0f;

    public float overdoseStepDur = 0.5f;
    public int overdoseThreshold = 100;
    private bool odEffect = false;
        private bool fadeOut = false;
        private float fadeTarget = 100.0f;

    public AudioSource wind, impact;
    private float initVolume = 1.0f;

    public static float playerVel = 0.0f;
    public static Character playerInstance;

    public int deathNegScore = 10;
    public int baseScore = 10;

    public VignetteAndChromaticAberration chromAb;
    public BloomAndFlares bloomF;
    public ColorCorrectionCurves colCorr;
    Vector3 velocity;
    public float controllSmoothness;
    public static Transform statdir;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        if (statdir == null)
            statdir = directions;
        if (directions == null)
            directions = statdir;
        initVolume = wind.volume;
        playerInstance = this;

        chromAb = Camera.main.GetComponent<VignetteAndChromaticAberration>();
        bloomF = Camera.main.GetComponent<BloomAndFlares>();
        colCorr = Camera.main.GetComponent<ColorCorrectionCurves>();

        GameData.Instance.reset();

        GameData.Instance.dead = false;
        Camera.main.GetComponent<CameraFollow>().setFollow(gameObject.transform);

        StartCoroutine(overdoseDecay());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!GameData.Instance.dead)
        {
            //  rb.AddForce((directions.forward * Input.GetAxis("Vertical") + directions.right * Input.GetAxis("Horizontal")) * thrust, ForceMode.Impulse);
            rb.velocity = Vector3.Lerp(rb.velocity, velocity + rb.velocity.y * Vector3.up, Time.fixedDeltaTime* controllSmoothness);
        }
    }

    void Update()
    {
        velocity = (directions.forward * Input.GetAxis("Vertical") + directions.right * Input.GetAxis("Horizontal")) * 40;
        if (rb.velocity.magnitude > maxVel)
            rb.velocity = rb.velocity.normalized * maxVel;

        if(odEffect)
        {
            if(!fadeOut) //during
            {
                chromAb.chromaticAberration = Mathf.Lerp(chromAb.chromaticAberration, fadeTarget, Time.deltaTime * 10.0f);
            } else //fade out
            {
                chromAb.chromaticAberration = Mathf.Lerp(chromAb.chromaticAberration, 2.0f, Time.deltaTime * 4.0f);
            }
        }

        wind.volume = gameObject.GetComponent<Rigidbody>().velocity.magnitude / maxVel * initVolume;

        playerVel = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.relativeVelocity.magnitude > 0.2f * maxVel)
        {
            SoundManager.playRandSound(impact,SoundManager.Instance.impact);
        }

        if (col.other.CompareTag("Ground"))
        {
            float factor = col.relativeVelocity.y;
            Debug.Log("Penus " + factor);
            Debug.Log("Penus2 " + playerInstance.rb.velocity.ToString());
            Debug.Log("sdhsdjkg "+col.relativeVelocity.ToString());
            GameData.Instance.score += (int)(baseScore * factor) * GameData.Instance.Multi;
            GetComponent<BoxCollider>().enabled = false;
            killThis();
            Debug.Log("Score: " + GameData.Instance.score);

            //
            GameObject penice = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);

            SoundManager.playRandSound(penice.GetComponents<AudioSource>()[0], SoundManager.Instance.impact);
            SoundManager.playRandSound(penice.GetComponents<AudioSource>()[1], SoundManager.Instance.blood);
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > maxVel / 2.0f)
            {
                SoundManager.playRandSound(penice.GetComponents<AudioSource>()[2], SoundManager.Instance.bone);

            }
            SoundManager.playRandSound(penice.GetComponents<AudioSource>()[3], SoundManager.Instance.brain);
        }
    }

    public void DoOverdose(int od, float heal)
    {
        GameData.Instance.overdose += od;
        GameData.Instance.health += heal;

        if(GameData.Instance.overdose >= overdoseThreshold)
        {
            GameData.Instance.overdose = overdoseThreshold;
            GameData.Instance.overDoseMulti++;
            odEffect = true;
            GameUI.UIes.OverdoseBool = true;

            fadeTarget = 100.0f;
            bloomF.enabled = true;
            colCorr.enabled = true;

            StartCoroutine(Overdosing());
            StartCoroutine(ODEffect());
            StopCoroutine(ODFadeOut());
        }
    }

    IEnumerator Overdosing()
    {
        Debug.Log("Overdosing! OD: " + GameData.Instance.overdose + "; HP: " + GameData.Instance.health);
        yield return new WaitForSeconds(overdoseStepDur);
        if(GameData.Instance.overdose > 0)
        {
            GameData.Instance.overdose -= 10;
            GameData.Instance.health -= 5.0f;
            StartCoroutine(Overdosing());
        } else
        {
            GameData.Instance.overdose = 0;

            bloomF.enabled = false;
            colCorr.enabled = false;
            GameUI.UIes.OverdoseBool = false;
            GameData.Instance.overDoseMulti--;

            StartCoroutine(ODFadeOut());
            StopCoroutine(ODEffect());
        }
    }

    IEnumerator ODEffect()
    {
        while (true)
        {
            fadeTarget = -fadeTarget * Random.Range(0.7f, 1.2f);
            yield return new WaitForSeconds(Random.Range(0.03f, 0.15f));
        }
    }

    IEnumerator ODFadeOut()
    {
        fadeOut = true;
        yield return new WaitForSeconds(5.0f);
        fadeOut = false;
        odEffect = false;
        chromAb.chromaticAberration = 2.0f;
    }

    IEnumerator overdoseDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            if (!odEffect && GameData.Instance.overdose > 0)
                GameData.Instance.overdose--;
        }
    }

    public void killThis()
    {
        if (!GameData.Instance.dead)
        {
            Debug.Log("anis: " + GameData.Instance.overkillMulti + "enus: " + GameData.Instance.health);
            if (GameData.Instance.overkillMulti == 1 && GameData.Instance.health > 0f)
            {
                Debug.Log("subbing " + (int)(deathNegScore * GameData.Instance.health));
                GameData.Instance.score -= (int)(deathNegScore * GameData.Instance.health);
            }
            GameData.Instance.dead = true;
        }
        /*GameData.highscoreEntry[] hscores = GameData.Instance.highscores;
        sortHighscores(hscores);
        if(hscores[hscores.Length-1].score < GameData.Instance.score)
        {
            //enter new score there and prompt for name...
        }*/
    }

    private void sortHighscores(GameData.highscoreEntry[] hs)
    {
        for(int i = 0; i < hs.Length; i++)
        {
            for(int j = 0; j < hs.Length; j++)
            {
                if(hs[j].score > hs[i].score)
                {
                    GameData.highscoreEntry tmp = hs[i];
                    hs[i] = hs[j];
                    hs[j] = tmp;
                }
            }
        }
    }
}
