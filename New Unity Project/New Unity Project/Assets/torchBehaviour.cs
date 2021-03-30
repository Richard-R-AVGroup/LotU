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
    private float randTime;

    // Start is called before the first frame update
    void Start()
    {
        maxLightFlicker = this.lightsource.range;
        randTime = Random.Range(0, 7);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (insideLant == true)
        {
            this.lightsource.intensity = Random.Range(minLightFlicker, maxLightFlicker);
        }

        if (outsideLant == true)
        {
            sunPos = GameObject.Find("Directional Light").GetComponent<Transform>().eulerAngles.x;
            if (sunPos > 10 && sunPos <= 90)
            {
                StartCoroutine("LightOff");
            }
            else if (sunPos >= 168 || sunPos <= 10)
            {
                StartCoroutine("LightUp");
            }
        }
    }

    private IEnumerator LightUp()
    {
        
        var em = emberSource.emission;
        
        yield return new WaitForSeconds(randTime);
        if (this.lightsource.enabled == false)
        {
            this.lightsource.enabled = true;
            em.enabled = true;
            randTime = Random.Range(0, 5);
        }
    }

    private IEnumerator LightOff()
    {
        
        var em = emberSource.emission;
        
        yield return new WaitForSeconds(randTime);
        if (this.lightsource.enabled == true)
        {
            this.lightsource.enabled = false;
            em.enabled = false;
            randTime = Random.Range(0, 3);
        }
    }
}
