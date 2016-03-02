using UnityEngine;
using System.Collections;

public class Thunderstorm : MonoBehaviour {

    //TODO: missing rain, thunder, clouds

    public Light dirLight;
    public Vector2 minMaxInterval = new Vector2(1.0f, 5.0f);
    public float flashDuration = 0.1f;
    public int maxBurst = 3;
    public Color lightningColor;
    public Vector2 lightningIntensity = new Vector2(5.0f, 10.0f);

	// Use this for initialization
	void Start () {
        StartCoroutine(countdown());
	}

    IEnumerator countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minMaxInterval.x, minMaxInterval.y));
            for (int i = 0; i < Random.Range(1, maxBurst); i++)
            {
                float tmpIntensity = dirLight.intensity;
                Color tmpClr = dirLight.color;
                dirLight.intensity = Random.Range(lightningIntensity.x, lightningIntensity.y);
                dirLight.color = lightningColor;
                yield return new WaitForSeconds(flashDuration);
                dirLight.intensity = tmpIntensity;
                dirLight.color = tmpClr;
                yield return new WaitForSeconds(flashDuration);
            }
        }
    }
}
