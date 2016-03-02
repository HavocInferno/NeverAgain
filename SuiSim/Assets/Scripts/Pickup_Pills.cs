using UnityEngine;
using System.Collections;

public class Pickup_Pills : Pickup {

    public int overdose = 10;
    public float heal = 4.0f;
    public int baseScore = 5;

    public override void Consume(GameObject player)
    {
        Character.playerInstance.DoOverdose(overdose, heal);
        GameData.Instance.score += baseScore * GameData.Instance.overkillMulti;
        Debug.Log("OD: " + GameData.Instance.overdose + "; HP: " + GameData.Instance.health);
    }
}