using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SongSelctBtn
{
    public class SongSelect_SelectBtn : UIButton
    {
        public GameObject stageUI;
        public override void ClickEvent()
        {
            OpenUI(stageUI);
            SoundManager.Instance.PlaySFXSound("metronome_tick");
        }
    }
}