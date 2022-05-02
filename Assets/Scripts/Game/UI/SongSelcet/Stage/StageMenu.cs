using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Song[] songList = null;
    public TMP_Text name = null;
    public TMP_Text composer = null;
    public TMP_Text level;
    public TMP_Text maxScore;
    public Image imgDisk = null;

    public int currentSong = 0;
    //public void Start()
    //{
    //    SettingSong();
    //}

    public StageMenu_SelectBtn selectBtn;
    UIManager uiManager;
    public void Bind(UIManager mgr)
    {
        uiManager = mgr;
        selectBtn.Bind(mgr);
    }

    public void SettingSong()
    {
        name.text = songList[currentSong].name;
        composer.text = songList[currentSong].composer;
        imgDisk.sprite = songList[currentSong].sprite;

        //AudioManager.instance.PlayBGM("BGM" + currentSong);
    }
}

