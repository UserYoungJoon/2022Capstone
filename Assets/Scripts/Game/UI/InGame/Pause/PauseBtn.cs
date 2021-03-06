using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn : UIButton
{
    private GameObject pauseUI;
    private UIManager uIManager;
    private ScoreUI scoreUI;
    public void Bind(GameObject pauseUI, UIManager uIManager, ScoreUI scoreUI)
    {
        this.pauseUI = pauseUI;
        this.uIManager = uIManager;
        this.scoreUI = scoreUI;
    }

    public override void ClickEvent()
    {
        OpenUI(pauseUI);
        CloseUI(scoreUI.gameObject);
        Debug.Log("Game Pause");
        Timer.FreezeTime();
    }
}