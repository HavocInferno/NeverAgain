using UnityEngine;
using System.Collections;

public class RazorPenis : MonoBehaviour {

    void Start()
    {
        SoundManager.playRandSound(GetComponent<AudioSource>(), SoundManager.Instance.cut);
        StartCoroutine(die());
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
