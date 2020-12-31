using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyBehaviour : MonoBehaviour
{
    public int health;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;                           //pretty self explanitory
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {

        }
    }
}
