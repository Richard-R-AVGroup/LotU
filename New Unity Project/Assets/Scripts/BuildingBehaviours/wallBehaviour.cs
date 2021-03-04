using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBehaviour : MonoBehaviour
{
    public bool hit;
    public bool sideWall;
    public Color setColor;
    public Material solidMat;
    public Material transparentMat;

    // Start is called before the first frame update
    void Start()
    {
        hit = false;                                                            //var if the wall has been hit
        setColor = this.GetComponent<MeshRenderer>().material.color;       //var for the color of the wall
    }

    // Update is called once per frame
    void Update()
    {
        if (hit == true)                                                        //if the wall is hit by raycast
        {
            ColourTransparent();                                                //set the alpha down
        }
        else                                                                    //if not
        {
            ColourOpaque();                                                     //set the alpha to max
        }
    }

    void ColourTransparent()                                                    //sets alpha to transparent
    {
        setColor.a = 0.2f;
        this.GetComponent<Renderer>().material = transparentMat;
        this.GetComponent<MeshRenderer>().material.color  = setColor;
    }

    void ColourOpaque()                                                         //sets alpha to max
    {
        this.GetComponent<Renderer>().material = solidMat;
        setColor.a = 1;
        this.GetComponent<MeshRenderer>().material.color = setColor;
    }
}
