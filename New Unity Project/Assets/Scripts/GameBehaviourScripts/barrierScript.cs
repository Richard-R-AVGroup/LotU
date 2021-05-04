using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class barrierScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("UpdateText").GetComponent<TMP_Text>().text = "No turning back now";
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(waitAndClear(2));
        }
    }

    private IEnumerator waitAndClear(float clearTime)
    {
        yield return new WaitForSeconds(clearTime);
        GameObject.Find("UpdateText").GetComponent<TMP_Text>().text = "";
    }
}
