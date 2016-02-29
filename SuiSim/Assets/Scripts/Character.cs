using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {

    private Rigidbody rb;
    public Transform directions;
    public float thrust = 1.0f;

    public GameObject deathEffect;
    public GameObject hitEffect;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        if (directions == null)
            directions = gameObject.transform;

        GameData.Instance.overkillMulti = 1;
        GameData.Instance.score = 0;
        GameData.Instance.health = 100.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetButton("Forward"))
        {
            rb.AddForce(directions.forward * thrust, ForceMode.Impulse);
        }
        if (Input.GetButton("Backward"))
        {
            rb.AddForce(-directions.forward * thrust, ForceMode.Impulse);
        }
        if (Input.GetButton("Left"))
        {
            rb.AddForce(-directions.right * thrust, ForceMode.Impulse);
        }
        if (Input.GetButton("Right"))
        {
            rb.AddForce(directions.right * thrust, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        } else
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }
}
