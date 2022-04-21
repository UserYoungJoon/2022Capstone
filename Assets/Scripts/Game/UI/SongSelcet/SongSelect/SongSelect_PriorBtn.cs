using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SongSelctBtn
{
    [System.Obsolete("�̱��� ��ư")]
    public class SongSelect_PriorBtn : UIButton
    {
        public override void ClickEvent()
        {
            
            SoundManager.Instance.PlaySFXSound("metronome_tick");
            /*
            //AudioManager.intance.PlaySFX("Touch");
            Debug.Log("Prior Song");

            if (--currentSong < 0)
                currentSong = songList.Length - 1;
            SettingSong(); */
        }
    }
}