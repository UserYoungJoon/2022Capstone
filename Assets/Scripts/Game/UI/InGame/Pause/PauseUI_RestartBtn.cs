using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI_RestartBtn : UIButton
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
        //Restart Code
        uiManager.SwitchGameStateToSelect();
        SoundManager.Instance.StopSongSound();
        uiManager.SwitchGameStateToGame();
        Timer.MeltTime();
        pauseUI.OpenScoreUI();
    }
}