using UnityEngine;
using System.Collections;

public class Helicock : MonoBehaviour {
    public Vector3 maxPos, minPos;
    public Vector3 direction;
    public float speed;
    bool active = true;
    public AudioSource source;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(minPos.x + Random.value * (maxPos.x - minPos.x), minPos.y + Random.value * (maxPos.y - minPos.y), minPos.z + Random.value * (maxPos.z - minPos.z));
        source.loop = true;
        StartCoroutine(start());
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x > maxPos.x || transform.position.x < minPos.x)
        {
            direction *= -1;
            float x = Mathf.Clamp(transform.position.x, minPos.x, maxPos.x);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        transform.LookAt(transform.position + direction);
	
	}
    void OnCollisionEnter(Collision col)
    {
        if (active && col.other.CompareTag("Player"))
        {
            active = false;
            GameData.Instance.overkillMulti++;
        }
        if (col.other.GetComponent<Rigidbody>() != null)
        {
            col.other.GetComponent<Rigidbody>().velocity = Vector3.right * Random.value * 60.0f + Vector3.forward * Random.value * 60.0f;
        }
    }
    IEnumerator start()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.playRandSound(source, SoundManager.Instance.heli);

    }
}
