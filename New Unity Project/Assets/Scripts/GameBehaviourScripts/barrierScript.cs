using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class barrierScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)                       //On collision with invisible border, display a message
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("UpdateText").GetComponent<TMP_Text>().text = "No turning back now";
        }
    }

    public void OnCollisionExit(Collision collision)                        //When player exits the collision with the border, start the countdown to clear message
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(waitAndClear(2));
        }
    }

    private IEnumerator waitAndClear(float clearTime)                       //Coroutine to wait 2 seconds before clearing the border message
    {
        yield return new WaitForSeconds(clearTime);
        GameObject.Find("UpdateText").GetComponent<TMP_Text>().text = "";
    }
}
