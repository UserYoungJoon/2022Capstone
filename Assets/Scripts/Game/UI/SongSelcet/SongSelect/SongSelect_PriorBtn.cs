using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SongSelctBtn
{
    [System.Obsolete("미구현 버튼")]
    public class SongSelect_PriorBtn : UIButton
    {
        public override void ClickEvent()
        {
            
            SoundManager.Instance.PlaySFXSound("metronome_tick");
       
            GameObject.Find("Canvas")
                .transform.Find("Select")
                .transform.Find("Disk")
                .transform.Find("Mask")
                .transform.Find("SongImage").GetComponent<SongSelect>().currentSong -= 1;
                


            Debug.Log("Preivous Song");
        }
    }
}