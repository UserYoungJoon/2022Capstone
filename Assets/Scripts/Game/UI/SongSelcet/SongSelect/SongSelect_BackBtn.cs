using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SongSelctBtn
{
    public class SongSelect_BackBtn : UIButton
    {
        public override void ClickEvent()
        {
            SoundManager.Instance.PlaySFXSound("metronome_tick");
            SceneManager.LoadScene("MainMenu");
        }
    }
}