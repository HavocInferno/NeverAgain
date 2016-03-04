using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class ClampSpeed : MonoBehaviour {
    Rigidbody rb;
    AudioSource bone, impact;
    static float maxspeed = 60f;
    public bool head;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bone = GetComponents<AudioSource>()[0];
        impact = GetComponents<AudioSource>()[1];
		rb.interpolation = RigidbodyInterpolation.Interpolate;
		if (head) {
			StartCoroutine(jump ());
		}
    }
	void FixedUpdate () {
		rb.velocity = rb.velocity - Vector3.up * rb.velocity.y *(0.2f*Time.fixedDeltaTime);
        if (rb.velocity.magnitude > maxspeed)
            rb.velocity = rb.velocity.normalized * maxspeed;
	}
    void OnCollisionEnter(Collision col)
    {
        if (!col.collider.CompareTag("Player") && !col.collider.CompareTag("Boundary"))
        {
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
	IEnumerator jump()
	{
		yield return new WaitForSeconds (0.5f);
		rb.AddForce (transform.up * -80 + transform.forward * -40, ForceMode.Impulse);
	}
}
