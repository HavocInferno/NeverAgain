using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

    private Vector3 initPos;
    public GameObject toFollow;
    public float dist = 8.0f;
	// Use this for initialization
	void Start () {
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y + 8.0f, toFollow.transform.position.z);
	}
}
