using UnityEngine;
using System.Collections;

public class ImpactZone : MonoBehaviour {

    public int baseScore = 100;

	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 impactVel = other.gameObject.GetComponent<Rigidbody>().velocity;
            float factor = impactVel.magnitude;
            GameData.Instance.score += baseScore * (int)factor * GameData.Instance.overkillMulti;
            this.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Score: " + GameData.Instance.score);
        }
    }
}
