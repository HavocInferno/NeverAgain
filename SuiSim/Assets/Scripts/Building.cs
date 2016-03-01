using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        Instantiate(EffectManager.Instance.glass, col.contacts[0].point, Quaternion.identity);
    }
}
