using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelColour : MonoBehaviour
{
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    float startTime;
    private bool isActvieCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActvieCollide){
            ActiveChangeColor();
            if(GetComponent<Renderer>().material.color == endColor){
                isActvieCollide = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            isActvieCollide = true;
        }
    }

    private void ActiveChangeColor()
    {
        float t = (Time.time - startTime * speed);
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        
    }
}
