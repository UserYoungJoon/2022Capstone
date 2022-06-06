using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu_SelectBtn : UIButton
{
    private UIManager uIManager;
    public GameManager gameManager;
    public StageMenu stgMenu;
    private string tmpCurrentSong;
    public void Bind(UIManager mgr)
    {
        uIManager = mgr;
    }

    public override void ClickEvent()
    {
        SoundManager.Instance.PlaySFXSound("metronome_tick"); // 꼭 SoundManager에 SFX Elements List에 추가해야되요!!
        
        if(stgMenu.GetCurrentSong().Equals("Airplane"))
        {
            tmpCurrentSong = "AirplaneFull";
            SoundManager.Instance.SetSongname(tmpCurrentSong);
        }
        else
        {
            SoundManager.Instance.SetSongname(stgMenu.GetCurrentSong());
        }
        gameManager.SetSelectSong(stgMenu.GetCurrentSong());
        // Debug.Log(stgMenu.GetCurrentSong());
        uIManager.SwitchGameStateToGame();
    }
}
