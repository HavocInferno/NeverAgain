using UnityEngine;
using System.Collections;
public class Collectible_Spawn : MonoBehaviour {

    public static Collectible_Spawn i;
    private Vector3 lastPos;
    public Transform initPos;
    private Vector3 initP;

    public Vector3 minBound = new Vector3(-5.0f, -20.0f, 5.0f);
    public Vector3 maxBound = new Vector3(5.0f, -10.0f, 15.0f);
    public float spread = 4.0f;
    public int maxInRow = 5;

    public GameObject[] pickups;
    private int numPickups;
    private float[] weightRange;
    private ArrayList list;

    // Use this for initialization
    public void reset()
    {
        foreach(Object i in list)
        {
            Destroy((GameObject)i);
        }
        list = new ArrayList();
        lastPos = new Vector3(initP.x, initP.y - 10, initP.z);
        StartCoroutine(Spawn());
    }
    void Start () {
        i = this;
        list = new ArrayList();
        initP = initPos.position;
        lastPos = new Vector3(initP.x, initP.y - 10, initP.z);
        numPickups = pickups.Length;

        computeWeights();

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

            float rnd = Random.value;
            for(int i = 0; i < pickups.Length; i++)
            {
                if(rnd <= weightRange[i])
                {
                    doLine(pickups[i], Random.Range(1, maxInRow), pos);
                    break;
                }
                rnd = rnd - weightRange[i];
            }
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
            list.Add(Instantiate(pickup, pos + i*dir, pickup.transform.rotation));
        }
    }

    void computeWeights()
    {
        float sum = 0.0f;
        for(int i = 0; i < pickups.Length; i++)
        {
            sum += pickups[i].GetComponent<Pickup>().spawnWeight;
        }

        weightRange = new float[pickups.Length];
        for(int i = 0; i < pickups.Length; i++)
        {
            weightRange[i] = pickups[i].GetComponent<Pickup>().spawnWeight / sum;
        }
    }

    void OnDrawGizmos()
    {
        if (initPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(initPos.position, initPos.position + new Vector3(1f, 0f, 0f) * minBound.x);
            Gizmos.DrawLine(initPos.position, initPos.position + new Vector3(0f, 1f, 0f) * minBound.y);
            Gizmos.DrawLine(initPos.position, initPos.position + new Vector3(0f, 0f, 1f) * minBound.z);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(initPos.position, initPos.position + new Vector3(1f, 0f, 0f) * maxBound.x);
            Gizmos.DrawLine(initPos.position, initPos.position + new Vector3(0f, 1f, 0f) * maxBound.y);
            Gizmos.DrawLine(initPos.position, initPos.position + new Vector3(0f, 0f, 1f) * maxBound.z);
        }
    }
}
