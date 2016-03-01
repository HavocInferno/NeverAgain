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

    public int overdoseThreshold = 100;
    private bool odEffect = false;
        private bool fadeIn = false;
        private bool fadeOut = false;
        private float fadeTarget = 100.0f;

    private bool dead = false;

    public AudioSource wind, impact;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        if (directions == null)
            directions = gameObject.transform;

        GameData.Instance.overkillMulti = 1;
        GameData.Instance.score = 0;
        GameData.Instance.health = 100.0f;

        StartCoroutine(overdoseDecay());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!dead)
        {
            rb.AddForce((directions.forward * Input.GetAxis("Vertical") + directions.right * Input.GetAxis("Horizontal")) * thrust, ForceMode.Impulse);
        }
    }

    void Update()
    {
        if(rb.velocity.magnitude > maxVel)
            rb.velocity = rb.velocity.normalized * maxVel;

        if(odEffect)
        {
            if(fadeIn) //fade in
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = Mathf.Lerp(Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration, 90.0f, Time.deltaTime * 2.0f);
            } else if(!fadeIn & !fadeOut) //during
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = Mathf.Lerp(Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration, fadeTarget, Time.deltaTime * 10.0f);
            } else if(fadeOut) //fade out
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = Mathf.Lerp(Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration, 2.0f, Time.deltaTime * 4.0f);
            }
        }

        gameObject.GetComponent<AudioSource>().volume = gameObject.GetComponent<Rigidbody>().velocity.magnitude / maxVel;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.relativeVelocity.magnitude > 0.2f * maxVel)
        {
            SoundManager.playRandSound(impact,SoundManager.Instance.impact);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            GameObject penice = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);

            SoundManager.playRandSound(penice.GetComponents<AudioSource>()[0], SoundManager.Instance.impact);
            SoundManager.playRandSound(penice.GetComponents<AudioSource>()[1], SoundManager.Instance.blood);
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > maxVel / 2.0f)
            {
                SoundManager.playRandSound(penice.GetComponents<AudioSource>()[2], SoundManager.Instance.bone);
                
            }
            SoundManager.playRandSound(penice.GetComponents<AudioSource>()[3], SoundManager.Instance.brain);
            //TODO: make dead and shit
        } /*else if(!other.CompareTag("Pickup") && !other.CompareTag("Impact"))
        {
            Instantiate(EffectManager.Instance.glass, transform.position, Quaternion.identity);
        }*/
    }

    public void DoOverdose(int od, float heal)
    {
        GameData.Instance.overdose += od;
        GameData.Instance.health += heal;

        if(GameData.Instance.overdose >= overdoseThreshold)
        {
            GameData.Instance.overdose = overdoseThreshold;
            GameData.Instance.overkillMulti++;
            odEffect = true;
            StartCoroutine(Overdosing());
            StartCoroutine(ODEffect());
        }
    }

    IEnumerator Overdosing()
    {
        Debug.Log("Overdosing! OD: " + GameData.Instance.overdose + "; HP: " + GameData.Instance.health);
        yield return new WaitForSeconds(0.5f);
        if(GameData.Instance.overdose > 0)
        {
            GameData.Instance.overdose -= 10;
            GameData.Instance.health -= 5.0f;
            StartCoroutine(Overdosing());
        } else
        {
            GameData.Instance.overdose = 0;
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
        Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 2.0f;
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
        dead = true;
    }
}
