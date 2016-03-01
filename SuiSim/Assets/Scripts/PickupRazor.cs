﻿using UnityEngine;
using System.Collections;

public class PickupRazor : Pickup {

    public float dmg = 5.0f;
    public int baseScore = 10;

	public override void Consume(GameObject player)
    {
        GameData.Instance.health -= dmg;
        GameData.Instance.score += baseScore * GameData.Instance.overkillMulti;
        //Debug.Log("Score: " + GameData.Instance.score);
    }
}
