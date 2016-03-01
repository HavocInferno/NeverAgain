using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public AudioSource source;
	// Use this for initialization
	void Start () {
        StartCoroutine(pest());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator pest()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            SoundManager.playRandSound(source, SoundManager.Instance.glass);
        }
    }
}
