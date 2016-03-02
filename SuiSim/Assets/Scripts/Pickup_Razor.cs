using UnityEngine;
using System.Collections;

public class Pickup_Razor : Pickup {

    public float dmg = 5.0f;
    public int baseScore = 10;

	public override void Consume(GameObject player)
    {
        GameData.Instance.score += baseScore * GameData.Instance.overkillMulti;
        GameData.Instance.health -= dmg;
        Debug.Log("HP: " + GameData.Instance.health);
    }
}
