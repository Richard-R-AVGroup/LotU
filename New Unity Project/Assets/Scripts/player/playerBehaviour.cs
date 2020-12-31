using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour

{

    public float speed;                 //speed to add to the velocity of the player
    public float maxSpeed;              //fastest speed the player can go

    public Vector3 weaponLocation;        // location of the weapon model

    public List<GameObject> inv;

    public GameObject weapon;

    void Start()
    {
        if (Application.isEditor)
        {
            Debug.Log("hello");
            weapon = Instantiate(Resources.Load<GameObject>("Assets/Objects/Weapons/basic_Iron_Sword") as GameObject);
        }
    }


    void Update()
    {
        characterMovement();            //function for the character movement based off of the keyboard key pressed
    }

    void FixedUpdate()
    {
        Vector3 rgMag = this.GetComponent<Rigidbody>().velocity;
        /*
         * if the character speed is below the maximum speed of the character:
         * -Add speed until it hits the ceiling
        */
        if (rgMag.magnitude >= maxSpeed)
        {
            this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }

    }

    /*
     * based on the WASD keys pressed on the keyboard;
     * add velocity to each direction based on the location of the key by the speed declared
     * w = north
     * d = east
     * s = south
     * a = west
    */
    public void characterMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.right * speed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.S))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.right * -speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.forward * -speed * Time.deltaTime);
        }
    }
}
