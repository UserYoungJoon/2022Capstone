using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_BackBtn : UIButton
{
    private UIManager uiManager;

    public void Bind(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public override void ClickEvent()
    {
        SoundManager.Instance.PlaySFXSound("metronome_tick");
        uiManager.SwitchGameStateToSelect(); //���� �ǳڵ��� �ߺ������ϰ�����. �����ϱ��� �����ؾߵ�, �ʵ� �ߺ�������
    }
}
