using UnityEngine;
using System.Collections;

public class ClampSpeed : MonoBehaviour {
    Rigidbody rb;
    static float maxspeed = 60.0f;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
	void FixedUpdate () {
        if (rb.velocity.magnitude > maxspeed)
            rb.velocity = rb.velocity.normalized * maxspeed;
	}
}
