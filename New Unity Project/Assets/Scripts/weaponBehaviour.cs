using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBehaviour : MonoBehaviour
{

    public GameObject Weapon;
    public Animator animator;
    public bool attack;
    public bool attacking;
    public int combo;

    // Start is called before the first frame update
    void Start()
    {
        Weapon = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("combo", combo);
        if (attack == true)
            animator.SetBool("attackStance", true);
        else if (attack == false)
            animator.SetBool("attackStance", false);

        if (attacking == true)
        {
            animator.SetBool("attacking", true);
        }
        else if (attacking == false)
        {
            animator.SetBool("attacking", false);
        }
    }
}
