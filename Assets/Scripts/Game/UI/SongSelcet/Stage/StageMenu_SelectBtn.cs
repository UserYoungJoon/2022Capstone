using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu_SelectBtn : UIButton
{
    private UIManager uIManager;
    public void Bind(UIManager mgr)
    {
        uIManager = mgr;
    }

    public override void ClickEvent()
    {
        uIManager.SwitchGameStateToGame();
    }
}
