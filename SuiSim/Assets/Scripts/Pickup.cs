using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    public GameObject onDeath;
    //public float fallVel = 15.0f;
    private float angleY = 0.0f;
    public Vector3 turnAxis = Vector3.up;
    public float spawnWeight = 1.0f;

    void Start()
    {
        angleY = 5.0f * Random.Range(-1.0f, 1.0f);
    }

    void Update()
    {
        if(transform.position.y > 0 && transform.position.y < Character.playerInstance.transform.position.y)
        {
            transform.Translate(Vector3.down * 0.5f*Character.playerVel * Time.deltaTime, Space.World);
        } else
        {
            Destroy(this.gameObject);
        }

        transform.Rotate(turnAxis, angleY);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Consume(other.gameObject);

            Instantiate(onDeath, transform.position, Quaternion.identity);

            this.gameObject.GetComponent<Collider>().enabled = false;
            //this.gameObject.GetComponent<Renderer>().enabled = false;

            Destroy(this.gameObject);
        }
    }

    public virtual void Consume(GameObject player) { }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + new Vector3(0f, 1f, 0f), "Pickup_Gizmo.tga", true);
    }
}
