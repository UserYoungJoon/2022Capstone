using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGameUI : MonoBehaviour
{
    public Finish_RetryBtn  retryBtn;
    public Finish_BackBtn   backBtn;

    public TMPro.TMP_Text   resultScore;
    public TMPro.TMP_Text   gameResult;
    public Image            songImage;

    private const string CLEAR_MESSAGE = "GAME CLEAR!";
    private const string OVER_MESSAGE = "GAME OVER!";

    public void Bind(UIManager uiManager)
    {
        retryBtn.Bind(uiManager);
        backBtn.Bind(uiManager);
    }

    public void SetBeforeOpen(int resultScore,bool isClearGame, Sprite songSprite)
    {
        this.resultScore.text = resultScore.ToString();
        if (isClearGame)
            gameResult.text = CLEAR_MESSAGE;
        else
            gameResult.text = OVER_MESSAGE;

        songImage.sprite = songSprite;
    }
}
