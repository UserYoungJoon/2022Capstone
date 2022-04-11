using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public ScoreUI scoreUI;
    private UIManager uiManager;

    public PauseUI_ResumeBtn resumeBtn;
    public PauseUI_RestartBtn restartBtn;
    public PauseUI_ExitBtn exitBtn;

    public void Bind(UIManager uiManager,ScoreUI scoreUI)
    {
        this.uiManager = uiManager;

        resumeBtn.Bind(this);
        restartBtn.Bind(uiManager,this);
        exitBtn.Bind(uiManager, this);
    }

    public void OpenScoreUI()
    {
        scoreUI.gameObject.SetActive(true);
    }

    public void CloseScoreUI()
    {
        scoreUI.gameObject.SetActive(false);
    }
}