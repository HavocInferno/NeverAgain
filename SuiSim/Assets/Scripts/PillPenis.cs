using UnityEngine;
using System.Collections;

public class PillPenis : MonoBehaviour {

    void Start()
    {
        SoundManager.playRandSound(GetComponent<AudioSource>(), SoundManager.Instance.fart);
        StartCoroutine(die());
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
