using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    [SerializeField]
    public AudioClip[] blood, brain, fart, glass, heli, airhorn, carhorn, bone, impact, plane, thunder, wind, environment, debis;
    public static SoundManager Instance;
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static AudioClip getSound(AudioClip[] field)
    {
        return field[Random.Range(0,field.Length)];
    }
    public static void playRandSound(AudioSource source, AudioClip[] field)
    {
        source.clip = getSound(field);
        source.Play();
    }
}
