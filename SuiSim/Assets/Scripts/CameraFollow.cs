using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform followedObject;
    private Vector3 toPosition;

    void LateUpdate()
    {
        toPosition = followedObject.position + Vector3.up * distanceUp - followedObject.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, toPosition, Time.deltaTime * smooth);
        //transform.LookAt(followedObject);
    }
}
