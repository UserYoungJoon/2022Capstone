using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    //In game
    public PauseUI pauseUI;
    public ScoreUI scoreUI;
    public PauseBtn pauseBtn;
    public FinishGameUI finishGameUI;

    //select
    public GameObject selectUI;
    public StageMenu stageUI;

    public void Init()
    {
        pauseUI.gameObject.SetActive(true);
        scoreUI.gameObject.SetActive(true);
        pauseBtn.gameObject.SetActive(true);
        finishGameUI.gameObject.SetActive(true);
        selectUI.gameObject.SetActive(true);
        stageUI.gameObject.SetActive(true);
    }

    public void Bind(GameManager mgr)
    {
        gameManager = mgr;
        stageUI.Bind(this);
        pauseUI.Bind(this, scoreUI);
        pauseBtn.Bind(pauseUI.gameObject, this, scoreUI);
        finishGameUI.Bind(this);

        pauseUI.gameObject.SetActive(false);
        scoreUI.gameObject.SetActive(false);
        pauseBtn.gameObject.SetActive(false);
        finishGameUI.gameObject.SetActive(false);
        selectUI.gameObject.SetActive(false);
        stageUI.gameObject.SetActive(false);
    }

    public void UpdateScoreUI(int plus, eScoreType scoreType)
    {
        scoreUI.UpdateUI(plus, scoreType);
    }

    public void OpenFinishGameUI(int resultScore, bool isClearGame, Sprite songSprite)
    {
        finishGameUI.SetBeforeOpen(resultScore, isClearGame, songSprite);
        finishGameUI.gameObject.SetActive(true);
        Timer.FreezeTime();
    }
    
    public void SwitchGameStateToGame()
    {
        gameManager.SwitchGameState(GameManager.eGameState.GAME);
    }

    public void SwitchGameStateToSelect()
    {
        gameManager.SwitchGameState(GameManager.eGameState.SELECT_SONG);
    }

    public void SetSelectMode()
    {
        //Deactivate
        pauseBtn.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(false);
        finishGameUI.gameObject.SetActive(false);
        scoreUI.gameObject.SetActive(false);

        //Activate
        selectUI.SetActive(true);

        //Set
    }

    public void SetGameMode()
    {
        //Deactivate
        selectUI.SetActive(false);
        stageUI.gameObject.SetActive(false);
        finishGameUI.gameObject.SetActive(false);

        //Activate
        pauseBtn.gameObject.SetActive(true);
        scoreUI.gameObject.SetActive(true);

        //Set
        scoreUI.SetBeforeStart();
    }
}
