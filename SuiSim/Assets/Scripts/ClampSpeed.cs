using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class ClampSpeed : MonoBehaviour {
    Rigidbody rb;
    AudioSource bone, impact;
    static float maxspeed = 60.0f;
    public bool head;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bone = GetComponents<AudioSource>()[0];
        impact = GetComponents<AudioSource>()[1];
    }
	void FixedUpdate () {
        if (rb.velocity.magnitude > maxspeed)
            rb.velocity = rb.velocity.normalized * maxspeed;
	}
    void OnCollisionEnter(Collision col)
    {
        if(!col.other.CompareTag("Player"))
        if (col.relativeVelocity.magnitude > 0.1f * maxspeed)
            SoundManager.playRandSound(impact, SoundManager.Instance.impact);

        if (col.relativeVelocity.magnitude > 0.3f * maxspeed)
        {
            Instantiate(EffectManager.Instance.blood, transform.position, transform.rotation);
        }
        if (col.relativeVelocity.magnitude > 0.6f * maxspeed)
        {
            if (head)
                SoundManager.playRandSound(bone, SoundManager.Instance.brain);
            else
                SoundManager.playRandSound(bone, SoundManager.Instance.bone);
        }
        
    }
}
