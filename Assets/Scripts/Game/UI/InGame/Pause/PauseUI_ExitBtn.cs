using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI_ExitBtn : UIButton
{
    private UIManager uiManager;
    private PauseUI pauseUI;
    public void Bind(UIManager uiManager, PauseUI pauseUI)
    {
        this.uiManager = uiManager;
        this.pauseUI = pauseUI;
    }

    public override void ClickEvent()
    {
        Timer.FreezeTime();
        SoundManager.Instance.StopSongSound();
        uiManager.SwitchGameStateToSelect();
    }
}