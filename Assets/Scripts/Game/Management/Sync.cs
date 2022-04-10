using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{

    AudioSource playTik;
    public AudioClip tik;
    float musicBPM = 105f;
    float standardBPM = 60f;
    float musicTempo = 2f;
    float standardTempo = 4f;
    float tikTime = 0;
    float nextTime = 0;
    void Start()
    {
        playTik = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ( 105 / 60 ) * (2 / 4) 4분의 2박자, 초당 재생수
        tikTime = (musicBPM / standardBPM) * (musicTempo / standardTempo);
        
        nextTime += Time.deltaTime;

        if(nextTime > tikTime)
        {
            StartCoroutine(PlayTik(tikTime));
            nextTime = 0;
        }

    }

    IEnumerator PlayTik(float tikTime)
    {
        Debug.Log(nextTime); // 시간 오차 확인용
        playTik.PlayOneShot(tik);
        yield return new WaitForSeconds(tikTime); // tikTime 만큼 대기
    }
}


// songposition = (float)(AudioSettings.dspTime – dsptimesong) * song.pitch – offset;
// Offset 공의 속도 조절 or 패널의 위치 조절