using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelColour : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    private const string CorotineName = "Fade";
    private bool isActvieCollide = false;
    public Material color;

    public void Init()
    {
        color = this.GetComponent<Renderer>().sharedMaterial;
    }
    public void Bind()
    {
        
    }
    public void TimingColor()
    {
        if(PlayerBall.inputTiming == true)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }
    public void ChangePanelColor(eScoreType scoreType)
    {
        if(scoreType == eScoreType.PERFECT)
        {
            color.SetColor("_Color", Color.blue);
        }

        else if(scoreType == eScoreType.GREAT)
        {
            color.SetColor("_Color", Color.green);
        }

        else if(scoreType == eScoreType.GOOD)
        {
            color.SetColor("_Color", Color.yellow);
        }

        else if(scoreType == eScoreType.BAD)
        {
            color.SetColor("_Color", Color.black);
        }
    }
    

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {

            // StartCoroutine(CorotineName);
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            
            // StopCoroutine(CorotineName);
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
