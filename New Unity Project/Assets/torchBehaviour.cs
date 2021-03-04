using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchBehaviour : MonoBehaviour
{

    public bool outsideLant;
    public bool insideLant;

    public Light lightsource;
    public ParticleSystem emberSource;

    private float maxLightFlicker;
    public float minLightFlicker;
    public float sunPos;

    // Start is called before the first frame update
    void Start()
    {
        maxLightFlicker = this.lightsource.range;
    }

    // Update is called once per frame
    void Update()
    {
        var em = emberSource.emission;
        if (insideLant == true)
        {
            this.lightsource.intensity = Random.Range(minLightFlicker, maxLightFlicker);
        }

        if (outsideLant == true)
        {
            sunPos = GameObject.Find("Directional Light").GetComponent<Transform>().rotation.x;
            if (sunPos >= 8 && sunPos <= 167)
            {
                this.lightsource.enabled = false;
                em.enabled = false;
            }
            else if (sunPos >= 168)
            {
                this.lightsource.enabled = true;
                em.enabled = true;
            }
        }
    }
}
