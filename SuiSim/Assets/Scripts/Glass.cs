using UnityEngine;
using System.Collections;

public class Glass : MonoBehaviour {
    void Start()
    {
        SoundManager.playRandSound(GetComponent<AudioSource>(), SoundManager.Instance.glass);
        StartCoroutine(die());
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
