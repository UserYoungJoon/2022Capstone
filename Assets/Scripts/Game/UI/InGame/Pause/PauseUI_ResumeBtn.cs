using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI_ResumeBtn : UIButton
{
    private PauseUI pauseUI;
    public void Bind(PauseUI pauseUI)
    {
        this.pauseUI = pauseUI;
    }

    public override void ClickEvent()
    {
        Timer.MeltTime();
        SoundManager.Instance.PlaySongSound();
        pauseUI.OpenScoreUI();
        CloseUI(pauseUI.gameObject);
    }
}