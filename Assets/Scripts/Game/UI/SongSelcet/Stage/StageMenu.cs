using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Song
{
    public string songname;
    public string composer;
    public string level; 
    //public string maxScore; 
    public Sprite sprite;
}
public class StageMenu : MonoBehaviour
{
    public Song[] songList = null;
    public TMP_Text songname = null;
    public TMP_Text composer = null;
    public TMP_Text level;
    //public TMP_Text maxScore;
    public Image imgDisk = null;

    public int currentSong = 0;
    public void Start()
    {
        SettingSong();
    }
    public StageMenu_SelectBtn selectBtn;
    UIManager uiManager;
    public void Bind(UIManager mgr)
    {
        uiManager = mgr;
        selectBtn.Bind(mgr);
    }

    public void SettingSong()
    {
        songname.text = songList[currentSong].songname;
        composer.text = songList[currentSong].composer;
        imgDisk.sprite = songList[currentSong].sprite;
        level.text = songList[currentSong].level;
        
        Debug.Log(songname.text.ToString());
    }

    public string GetCurrentSong()
    {
        string tmp = songname.text.ToString();
        return tmp;
    }
    public void Update()
    {
        if (currentSong == songList.Length)
            currentSong = 0;
        //SettingSong();

        else if (currentSong < 0)
            currentSong = songList.Length - 1;
        SettingSong();
    }

}

