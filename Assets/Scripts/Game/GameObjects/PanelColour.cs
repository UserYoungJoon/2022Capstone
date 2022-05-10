using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelColour : MonoBehaviour
{
    private const string CorotineName = "Fade";
    private bool isActvieCollide = false;
    public Material color;
    public GameObject panel;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(PlayerBall.inputTiming == true)
            {
                Debug.Log("인풋 타이밍 트루고 지금 누르셈");
                panel.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }
    }

}
