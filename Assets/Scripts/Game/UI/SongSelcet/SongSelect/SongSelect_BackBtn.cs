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
            SceneManager.LoadScene("MainMenu");
        }
    }
}