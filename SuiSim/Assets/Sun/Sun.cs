using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
    public float cycleLength;
    public LensFlare flare;
    float flareBright;
    Color color;
	// Use this for initialization
	void Start () {
        color = flare.color;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up,360.0f/cycleLength*Time.deltaTime, Space.Self);
        flareBright = (transform.forward.y * -5f)+0.3f;
      //  Debug.Log(flareBright);
        flareBright = Mathf.Max(0,Mathf.Min(1.0f, flareBright));
        color.b = flareBright;
        color.g = flareBright;
        flare.color = color;
        flare.brightness = flareBright;
	}
}
