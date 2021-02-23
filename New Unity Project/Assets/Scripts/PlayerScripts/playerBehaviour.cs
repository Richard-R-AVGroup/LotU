using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour

{

    public float speed;                 //speed to add to the velocity of the player
    public float maxSpeed;              //fastest speed the player can go
    public float walkingRot;       //holder for the rotation.y variable
    public float attackSpeed;
    public bool isAttacking;
    public bool turn;
    public bool walk;

    public List<GameObject> inv;

    public GameObject weapon;
    public GameObject weaponLocation;      // location of the weapon model

    public Camera camera;

    void Start()
    {
        if (Application.isEditor)
        {
            weapon = Instantiate(GameObject.Find("ObjectLibrary").GetComponent<gameObjLibrary>().basicIronSword, weaponLocation.transform.position, weaponLocation.transform.rotation, weapon.transform);
        }

        attackSpeed = 3;
    }


    void Update()
    {
        characterMovement();            //function for the character movement based off of the keyboard key pressed
        buttonCheck();
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
            walkingRot = 180;
            if (Input.GetAxis("Vertical") > 0 && walk == false)
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, walkingRot, 0, 0), 1);
            turn = true;
        }

        if (Input.GetKey(KeyCode.S))
        {

            this.GetComponent<Rigidbody>().AddForce(Vector3.right * -speed * Time.deltaTime);
            walkingRot = 0;
            if (Input.GetAxis("Vertical") < 0 && walk == false)
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, walkingRot, 0, 0), 1);
            turn = true;

        }

        if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed * Time.deltaTime);
            walkingRot = 90;
            if (Input.GetAxis("Horizontal") < 0 && walk == false)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 1);
            turn = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.forward * -speed * Time.deltaTime);
            walkingRot = 270;
            if (Input.GetAxis("Horizontal") > 0 && walk == false)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 1);
            turn = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 lookTarget;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Ground");


            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                lookTarget = hit.point;
                transform.LookAt(lookTarget);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
            }


            maxSpeed = (3);
            walk = true;

            if (Input.GetMouseButtonDown(0))
            {
                attackSpeed = 3;
                weapon.GetComponent<weaponBehaviour>().attack = true;
                weapon.GetComponent<weaponBehaviour>().attacking = true;

                if (weapon.GetComponent<weaponBehaviour>().combo == 0)
                    weapon.GetComponent<weaponBehaviour>().combo ++;

                else if (weapon.GetComponent<weaponBehaviour>().combo == 1)
                    weapon.GetComponent<weaponBehaviour>().combo ++;

                else if (weapon.GetComponent<weaponBehaviour>().combo == 2)
                    weapon.GetComponent<weaponBehaviour>().combo --;

                isAttacking = true;
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed = 7;
            walk = false;
        }

        if (!Input.anyKey)
            turn = false;
    }

    public void buttonCheck()
    {
        if(isAttacking == true)
        {
            attackSpeed -= 1f * Time.deltaTime;
        }

        if (attackSpeed <= 0)
        {
            weapon.GetComponent<weaponBehaviour>().attack = false;
            weapon.GetComponent<weaponBehaviour>().attacking = false;
            weapon.GetComponent<weaponBehaviour>().combo = 0;
            isAttacking = false;
            attackSpeed = 3;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameObject.Find("GameManager").GetComponent<gmBehaviour>().isPaused = true;
        }
    }

    public float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.x - a.x, b.y - a.y) * Mathf.Rad2Deg;
    }
}