using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gmBehaviour : MonoBehaviour
{

    public static gmBehaviour instance = null;
    public bool isPaused;
    public bool debug;

    public int test = 4;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debug == false)
        {
            if (isPaused == true)
            {
                Time.timeScale = 0;
                GameObject.Find("Directional Light").GetComponent<sunBehaviour>().sunSpeed = 0;
            }
            else
            {
                Time.timeScale = 1;
                GameObject.Find("Directional Light").GetComponent<sunBehaviour>().sunSpeed = 0.1f;
            }
        }
    }
}
