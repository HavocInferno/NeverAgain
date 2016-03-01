using UnityEngine;
using System.Collections;

public class Collectible_Spawn : MonoBehaviour {

    private Vector3 lastPos;
    public Transform initPos;
    private Vector3 initP;

    public Vector3 minBound = new Vector3(-5.0f, -20.0f, 5.0f);
    public Vector3 maxBound = new Vector3(5.0f, -10.0f, 15.0f);
    public float spread = 4.0f;
    public int maxInRow = 5;

    public GameObject[] pickups;
    private int numPickups;

    // Use this for initialization
    void Start () {
        initP = initPos.position;
        lastPos = new Vector3(initP.x, initP.y - 10, initP.z);
        numPickups = pickups.Length;
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()
    {
        while (lastPos.y > 0)
        {
            //Debug.Log("Go");
            float x = Random.Range(minBound.x, maxBound.x);
            float y = Random.Range(minBound.y, maxBound.y);
            float z = Random.Range(minBound.z, maxBound.z);
            Vector3 pos = new Vector3(initP.x + x, lastPos.y + y, initP.z + z);
            doLine(pickups[Random.Range(0, numPickups)], Random.Range(1, maxInRow), pos);
            lastPos = pos;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void doLine(GameObject pickup, int num, Vector3 pos)
    {
        Vector3 dir = Vector3.forward * Random.Range(-spread, spread) 
                    + Vector3.up * Random.Range(-spread, spread) 
                    + Vector3.right * Random.Range(-spread, spread);
        for (int i = 0; i < num; i++)
        {
            Instantiate(pickup, pos + i*dir, Quaternion.identity);
        }
    }
}
