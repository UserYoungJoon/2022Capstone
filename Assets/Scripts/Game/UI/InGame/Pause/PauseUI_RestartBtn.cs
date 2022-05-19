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
        uiManager.SwitchGameStateToSelect(); //현재 판넬들을 중복생성하고있음. 생성하기전 삭제해야됨, 맵도 중복생성중
        SoundManager.Instance.StopSongSound();
        uiManager.SwitchGameStateToGame();
        Timer.MeltTime();
        pauseUI.OpenScoreUI();
    }
}