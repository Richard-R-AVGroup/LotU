using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public bool open;
    public float close;

    void Start()
    {
        animator = this.GetComponent<Animator>();       //finds the objects animator
    }

    // Update is called once per frame
    void Update()
    {
        
        if (open == true)                                                               //if the door is open
        {
            animator.SetBool("Open", true);                                             //set the animators var to true
            close += Time.deltaTime;                                                    //run the countdown timer to close the door automatically

            if(close >= 3)                                                              //if the timer is more than 3 seconds
            {
                open = false;                                                           //door definetaly isnt open
                close = 0;                                                              //reset timer
            }    
        }
        else
            animator.SetBool("Open", false);                                            //if open aint true set the animator var to false (closes door)
    }
    /*
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.Space))         //if the player is colliding with the door and presses space
        {
            this.open = true;                                                           //set the door to open (read upwards)
        }
    }*/
}
