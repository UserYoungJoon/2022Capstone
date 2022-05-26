using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelColour : MonoBehaviour
{
    public GameObject panel;

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(PlayerBall.inputTiming == true)
            {
                panel.transform.Find("ring").GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
        }
    }

}
