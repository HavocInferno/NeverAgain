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

    private float angleZ, angleX = 0f;
    private Quaternion initRot;

    void Start()
    {
        initRot = transform.localRotation;
        StartCoroutine(Rd());
    }

    void LateUpdate()
    {
        toPosition = followedObject.position + Vector3.up * distanceUp - followedObject.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, toPosition, Time.deltaTime * smooth);
        transform.Rotate(Vector3.up * angleZ + Vector3.right * angleX);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, initRot, Time.deltaTime * 2.0f);
        //transform.LookAt(followedObject);
    }

    IEnumerator Rd()
    {
        while (true)
        {
            float factor = followedObject.gameObject.GetComponent<Rigidbody>().velocity.magnitude / followedObject.GetComponent<Character>().maxVel;
            angleX = factor * Random.Range(-0.3f, 0.3f);
            angleZ = factor * Random.Range(-0.3f, 0.3f);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.05f));
        }
    }

    public void setFollow(Transform penus)
    {
        followedObject = penus;
    }
}
