using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_RetryBtn : UIButton
{
    private UIManager uiManager;

    public void Bind(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public override void ClickEvent()
    {
        uiManager.SwitchGameStateToSelect(); //현재 판넬들을 중복생성하고있음. 생성하기전 삭제해야됨, 맵도 중복생성중
        uiManager.SwitchGameStateToGame();
    }
}
