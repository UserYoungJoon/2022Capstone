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
    public GameObject panel;

    public void Init()
    {
        color = this.GetComponent<Renderer>().sharedMaterial;
    }
    public void Bind()
    {
        
    }
    public void ChangePanelColor(eScoreType scoreType)
    {
        // 판정에 따른 패널 색 변화
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
