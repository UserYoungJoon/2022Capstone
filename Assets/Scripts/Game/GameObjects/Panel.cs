using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public AudioSource audioSource;
    static int index;
    private GameObject Beatsmap; //= GameObject.Find("Beatsmap"); 
    public void SetBeforeRun()
    {
        Beatsmap = GameObject.Find("Beatsmap");
        //5번부터 판넬 다 끔(판넬 5개가 켜진 상태)
        for (index = 5; index < Beatsmap.transform.childCount; index++) 
        {
            Beatsmap.transform.GetChild(index).gameObject.SetActive(false);
        }
        index = 5;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
          audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //판넬 1개를 끄고 다음 판넬을 키자
        Beatsmap = GameObject.Find("Beatsmap");
        Beatsmap.transform.GetChild(index-5).gameObject.SetActive(false);
        if (index < Beatsmap.transform.childCount)  //인덱스 초과 방지
        {
            Beatsmap.transform.GetChild(index++).gameObject.SetActive(true);
        }
    }
}