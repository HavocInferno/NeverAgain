using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    //public PackSize packSize = PackSize.MEDIUM;
    public GameObject onDeath;
    public float fallVel = 45.0f;

    public enum PackSize
    {
        SMALL,
        MEDIUM,
        LARGE
    }

    void Update()
    {
        if(transform.position.y > 0)
        {
            transform.Translate(Vector3.down * fallVel * Time.deltaTime);
        } else
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Consume(other.gameObject);

            Instantiate(onDeath, transform.position, Quaternion.identity);

            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<Renderer>().enabled = false;

            Destroy(this.gameObject);
        }
    }

    public virtual void Consume(GameObject player) { }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + new Vector3(0f, 1f, 0f), "Pickup_Gizmo.tga", true);
    }
}
