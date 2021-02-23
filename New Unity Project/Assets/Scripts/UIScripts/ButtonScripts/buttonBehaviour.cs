using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonBehaviour : MonoBehaviour
{

    public bool startButt;
    public bool quitButt;
    public bool resumeButt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick()
    {
        if (startButt == true)
            SceneManager.LoadScene(1);

        if (quitButt == true)
            Application.Quit();

        if (resumeButt == true)
            GameObject.Find("GameManager").GetComponent<gmBehaviour>().isPaused = false;
    }
}
