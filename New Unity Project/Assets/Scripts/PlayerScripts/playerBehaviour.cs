using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour

{

    public float speed;                 //speed to add to the velocity of the player
    public float maxSpeed;              //fastest speed the player can go
    public float walkingRot;            //holder for the rotation.y variable
    public float attackSpeed;           //Player attack speeed

    public bool talking;                //If the player is talking
    public bool isAttacking;            //Is the players weapon out
    public bool turn;                   //Is the player model allowed to turn
    public bool walk;                   //Is the player walking

    public List<GameObject> inv;        //Player Inventory

    public GameObject weapon;           //Weapon the player has equipped
    public GameObject weaponLocation;   //Location of the weapon model
    public GameObject playerEyes;       //Camera location for when the player enters conversation
    public GameObject camTarget;        //Where the camera will look at when player enters conversation
    public GameObject textLib;          //Reference to the dialogue library being pulled for conversation

    public Camera playerCamera;         //Main Camera

    void Start()
    {
        if (Application.isEditor)
        {
            weapon = Instantiate(GameObject.Find("ObjectLibrary").GetComponent<gameObjLibrary>().basicIronSword, weaponLocation.transform.position, weaponLocation.transform.rotation, weapon.transform);
        }

        attackSpeed = 3;
        talking = false;
    }


    void Update()
    {
        
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
        if (talking == false)
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
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
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
                        weapon.GetComponent<weaponBehaviour>().combo++;

                    else if (weapon.GetComponent<weaponBehaviour>().combo == 1)
                        weapon.GetComponent<weaponBehaviour>().combo++;

                    else if (weapon.GetComponent<weaponBehaviour>().combo == 2)
                        weapon.GetComponent<weaponBehaviour>().combo--;

                    isAttacking = true;
                }
            }
        }
        else return;

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
        characterMovement();            //function for the character movement based off of the keyboard key pressed

        if (isAttacking == true)
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
            if (talking == false)                                                               //If not talking 
                GameObject.Find("GameManager").GetComponent<gmBehaviour>().isPaused = true;     //Pause the game
            else                               
            {                                       //If player is talking
                StopCoroutine("talkingCamera");     //Stop pushing talkingCamera coroutine
                StartCoroutine(exitConvo());        //Start exiting the conversation
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Door" && Input.GetKey(KeyCode.E))      //When touching the door and pressing "E" (should probly change this to Trigger)
        {
            collision.gameObject.GetComponent<doorBehaviour>().open = true;         //Get the door to start opening Anim
        }
        else
            return;

    }

    private void OnTriggerStay(Collider other)                                                              
    {
        {
            if (other.gameObject.tag == "NPC" && Input.GetKey(KeyCode.E))                                           //When colliding with Npc trigger and press "E"
            {

                camTarget = other.gameObject;                                                                           //Set local camTarget gameObject var to this object 
                playerCamera.GetComponent<cameraBehaviour>().move = true;                                               //Set camera to allow movement
                other.GetComponent<npcPathfinding>().talking = true;                                                    //Set Npc talking to true, stopping its movement
                other.GetComponent<npcPathfinding>().goal = this.gameObject.GetComponent<Transform>().transform;        //Set NPC pathfinding goal to player to 
                textLib.GetComponent<textLibrary>().talk(camTarget, 1);                                                 //Start the Npc text dialouge conversation
                StartCoroutine("talkingCamera");                                                                        //Start camera movement to look at Npc
                talking = true;
            }
        }
    }

    private IEnumerator talkingCamera()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 * Time.deltaTime);
            playerCamera.transform.position = Vector3.Slerp(playerCamera.transform.position, playerEyes.transform.position, 0.3f);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, playerEyes.transform.rotation, 0.3f);
            Quaternion target = Quaternion.LookRotation(transform.position - camTarget.transform.position, Vector3.up) * Quaternion.Euler(0,-90,0);
            target.x = 0;
            target.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 8);
        }
    }

    private IEnumerator exitConvo()                                     //Called when selecting goodbye convo option or press escape button
    {
        textLib.GetComponent<textLibrary>().talk(camTarget, 0);         //Set the text library to its exit coroutine
        yield return new WaitForSeconds(1);                             //Wait for a second
        talking = false;                                                //Set talking var to false
        playerCamera.GetComponent<cameraBehaviour>().move = false;      //Set camera to go back to normal position
    }
}