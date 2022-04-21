using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu_BackBtn : UIButton
{
    public GameObject stageUI;
    public override void ClickEvent()
    {
        SoundManager.Instance.PlaySFXSound("metronome_tick");
        CloseUI(stageUI);
    }
}
