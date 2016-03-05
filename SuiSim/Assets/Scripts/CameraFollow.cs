using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    private float smooth2 =1;
    [SerializeField]
    private Transform followedObject;
    [SerializeField]
    private Transform spawnPosition;
    private Quaternion followrotation;
    private Vector3 toPosition;

    private float angleZ, angleX = 0f;
    private Quaternion initRot;
    private float gameFOV = 60.0f;
    public float menuFOV;

    void Start()
    {
        initRot = transform.localRotation;
        if (followedObject == null)
            setFollow(GameData.Instance.spawnPosition); //dummy, until first player is spawned
        StartCoroutine(Rd());
        followrotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (GameData.Instance.state == GameData.GameState.Playing)
        {
            smooth2 = Mathf.Lerp(smooth2, smooth, Time.deltaTime*0.4f);
            transform.rotation = Quaternion.Lerp(transform.rotation, followrotation, Time.fixedDeltaTime * smooth2);
			GetComponent<Camera>().fov = Mathf.Lerp(GetComponent<Camera>().fov, gameFOV, Time.fixedDeltaTime*smooth2);
            toPosition = followedObject.position + Vector3.up * distanceUp - followedObject.forward * distanceAway;
			transform.position = Vector3.Lerp(transform.position, toPosition, Time.fixedDeltaTime * smooth2);
            transform.Rotate(Vector3.up * angleZ + Vector3.right * angleX);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, initRot, Time.fixedDeltaTime * 2.0f);
            GetComponent<Camera>().nearClipPlane = .5f;
        }
        else
        {
            if (GameData.Instance.state == GameData.GameState.MainMenu)
            {

                smooth2 = 1.5f;
				GetComponent<Camera>().fov = Mathf.Lerp(GetComponent<Camera>().fov, menuFOV, Time.fixedDeltaTime*smooth2);
				transform.position = Vector3.Lerp(transform.position, spawnPosition.position, Time.fixedDeltaTime * smooth2);
				transform.rotation = Quaternion.Slerp(transform.rotation, spawnPosition.rotation, Time.fixedDeltaTime * smooth2);
                GetComponent<Camera>().nearClipPlane = .1f;
            }
            if (GameData.Instance.state == GameData.GameState.Stats)
            {

                smooth2 = 1.5f;
				GetComponent<Camera>().fov = Mathf.Lerp(GetComponent<Camera>().fov, menuFOV, Time.fixedDeltaTime * smooth2/4.0f);
				transform.position = Vector3.Lerp(transform.position, spawnPosition.position, Time.fixedDeltaTime * smooth2 / 4.0f);
				transform.rotation = Quaternion.Slerp(transform.rotation, spawnPosition.rotation, Time.fixedDeltaTime * smooth2 / 4.0f);
                GetComponent<Camera>().nearClipPlane = .1f;
            }

        }
        //transform.LookAt(followedObject);
    }

    IEnumerator Rd()
    {
        while (true)
        {
            float factor = 1.0f;
            if (followedObject.GetComponent<Character>() != null)
                factor = followedObject.gameObject.GetComponent<Rigidbody>().velocity.magnitude / followedObject.GetComponent<Character>().maxVel;
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
