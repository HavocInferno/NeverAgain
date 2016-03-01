using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public GameObject blood, glass, debris, dust;

    void Start()
    {
        Instance = this;
    }
}