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
    public bool repeatable = false;
    public SpriteRenderer fadeImage;
    void Start()
    {
        startTime = Time.time;
        StartCoroutine("ActiveChangeColor");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            isActvieCollide = true;

            
            if(isActvieCollide)
            {
                // ActiveChangeColor();
                StartCoroutine("fade");
            }
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            StopCoroutine("fade");
        }    
    }


    public IEnumerator fade()
    {
        Debug.Log("Enumerator started");
        float timer = 0.0f;
        float time = 0.5f;

        while(timer <= time)
        {
            timer += Time.deltaTime;
            float lerp_Percentage = timer / time;

            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, lerp_Percentage);

            yield return null;
        }
    }
}
