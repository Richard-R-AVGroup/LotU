using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textLibrary : MonoBehaviour
{

    public int cT;
    public int i;
    

    // Start is called before the first frame update
    void Start()
    {
        cT = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void talk(GameObject g, int i)
    {
        if (g == null)
        {
            Debug.Log("No gameObject as target");
            return;
        }

        TMP_Text t;
        t = g.GetComponent<npcPathfinding>().diaBox;
        
        if (g.GetComponent<npcPathfinding>().aiType.ToString() == "Shopkeep")                   //text path for the tutorial shopkeeper
        {
            switch (i)
            {
                case 1:
                    if (cT == 1)
                    {
                        if (g.GetComponent<npcPathfinding>().met == false)                  //**********something is wrong here**************//
                        {
                            t.text = "Oh Hello, How are you?";
                        }
                        else
                        {
                            t.text = "Heey good to see you again!";
                            cT += 2;
                        }
                    }
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        cT += 1;
                        Debug.Log(cT);
                        switch (cT)
                        {
                            case 2:
                                t.text = "The name is Richard,";
                                break;
                            case 3:
                                t.text = "You dont look like your from around here..";
                                break;
                            case 4:
                                t.text = "How can I help you?";
                                break;
                        }
                    }
                    break;

                case 0:
                    cT = 0;
                    int r = 0;                              //random Int
                    r = Random.Range(0, 3);                 //set int from 0 - 3
                    r = Mathf.RoundToInt(r);                //round it
                    Debug.Log("NPC exit number: " + r);     //show number in long *****DELETE LATER*****
                    switch (r)                              //give a different text for whatever random int is chosen
                    {
                        case 0:
                            t.text = "Have a good one!";
                            StartCoroutine(exitConvo());
                            break;
                        case 1:
                            t.text = "Take Care!";
                            StartCoroutine(exitConvo());
                            break;
                        case 2:
                            t.text = "See You Around!";
                            StartCoroutine(exitConvo());
                            break;
                    }
                        break;

                default:
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        i = 0;
                    }
                    return;
                    
            }
        }

        IEnumerator exitConvo()
        {
            cT = 0;
            i = 0;
            
            yield return new WaitForSeconds(0.7f);
            g.GetComponent<npcPathfinding>().met = true;
            t.text = "";
            g = null;
            
            
        }
    }

    
}
