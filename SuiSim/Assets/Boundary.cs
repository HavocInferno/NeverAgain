using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	public enum BoundDirection
    {
        LEFT,
        RIGHT,
        FRONT
    }

    public BoundDirection side;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("oi");
            switch (side)
            {
                case BoundDirection.RIGHT:
                    Character.playerInstance.rb.velocity = new Vector3(5f, Character.playerInstance.rb.velocity.y, Character.playerInstance.rb.velocity.z);
                    break;
                case BoundDirection.LEFT:
                    Character.playerInstance.rb.velocity = new Vector3(-5f, Character.playerInstance.rb.velocity.y, Character.playerInstance.rb.velocity.z);
                    break;
                case BoundDirection.FRONT:
                    Character.playerInstance.rb.velocity = new Vector3(Character.playerInstance.rb.velocity.x, Character.playerInstance.rb.velocity.y, -5f);
                    break;
            }
        }
    }
}
