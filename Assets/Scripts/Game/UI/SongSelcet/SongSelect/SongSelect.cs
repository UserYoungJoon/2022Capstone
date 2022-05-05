using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SongSelect : UIButton
{
    public List<Sprite> imageList;
    public int currentSong;

    // Init Bind를 통한 초기화가 필요할 듯?

    private void Awake() 
    {
        currentSong = 0;
    }

    private void Update() 
    {   
        // List.Count 호출 시 list의 Element 개수 호출, 현재 4개이므로 (0, 1, 2, 3).m 4는 존재하지 않으므로 0으로
        if(currentSong == imageList.Count)
        {
            currentSong = 0;
        }


        // -1 일때의 예외처리 필요함
        if(currentSong == -1)
        {
            Debug.Log("-1");
            currentSong = 4;
        }
        
        GetComponent<Image>().sprite = imageList[currentSong];
    }

    
}
