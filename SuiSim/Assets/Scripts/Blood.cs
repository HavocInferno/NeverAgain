using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {
    void Start()
    {
        SoundManager.playRandSound(GetComponent<AudioSource>(), SoundManager.Instance.blood);
        StartCoroutine(die());
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
