using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunBehaviour : MonoBehaviour
{
    public Vector3 sunRot;

    public int Hour;
    public float sunSpeed;

    // Start is called before the first frame update
    void Start()
    {
        sunRot = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        sunRot.x += sunSpeed;
        this.transform.eulerAngles = sunRot;


    }
}
