using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Song
{
    public string name;
    public string composer;
    public int bpm; //bpm이 실질적으로 게임 내 비트를 결정
    public Sprite sprite;
}
public class StageMenu : MonoBehaviour
{
    [SerializeField] Song[] songList = null;

    [SerializeField] Text txtSongName = null;

    [SerializeField] Text txtSongComposer = null;
    [SerializeField] Image imgDisk = null;

   // [SerializeField] GameObject TitleMenu = null;
    

    int currentSong = 0;

    void Start()
    {
        SettingSong();

    }
    public void BtnNext()
    {
        //AudioManager.intance.PlaySFX("Touch");
        Debug.Log("Next Song");
        if (++currentSong > songList.Length - 1)
            currentSong = 0;
        SettingSong();

    }
    public void BtnPrior()
    {
        //AudioManager.intance.PlaySFX("Touch");
        Debug.Log("Prior Song");

        if (--currentSong < 0)
            currentSong = songList.Length - 1;
        SettingSong();
    }

    void SettingSong()
    {
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;
        imgDisk.sprite = songList[currentSong].sprite;

        //AudioManager.instance.PlayBGM("BGM" + currentSong);
    }



   
}
