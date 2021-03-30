using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuBehaviour : MonoBehaviour
{

    public Vector3 pausePos;
    public Vector3 unpausePos;
    public bool pause;
    float yPos;

    // Start is called before the first frame update
    void Start()
    {
        pausePos = this.GetComponent<Transform>().position;
        unpausePos = pausePos + new Vector3(0, 700, 0);
        transform.position = unpausePos;
    }

    // Update is called once per frame
    void Update()
    {
        pause = GameObject.Find("GameManager").GetComponent<gmBehaviour>().isPaused;
        if (pause == true)
        {
            transform.position = Vector3.Lerp(this.transform.position, pausePos, 0.1f);

        }
        else
        {
            transform.position = Vector3.Lerp(this.transform.position, unpausePos, 0.2f);
        }
    }
}
