using UnityEngine;
using System.Collections;

public class DirsHelper : MonoBehaviour {

    void OnDrawGizmos()
    {
        //BLUE = FORWARD
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 5));

        //RED = RIGHT
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.right * 5));
    }
}
