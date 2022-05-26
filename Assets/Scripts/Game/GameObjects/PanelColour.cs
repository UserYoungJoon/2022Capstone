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
                panel.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }
    }

}
