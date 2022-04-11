using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelColour : MonoBehaviour
{
    public Color startColor;
    public Color endColor;

    private const string CorotineName = "Fade";
    private bool isActvieCollide = false;
    

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            // 유효하지 않은 코드
            //isActvieCollide = true;
            //if(isActvieCollide)
            //{
            //}
            StartCoroutine(CorotineName);
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            StopCoroutine(CorotineName);
        }    
    }


    private IEnumerator Fade()
    {
        float timer = 0.0f;
        float time = 0.3f;

        while(timer <= time)
        {
            timer += Time.deltaTime;
            float lerp_Percentage = timer / time;

            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, lerp_Percentage);

            yield return null;
        }
    }
}
