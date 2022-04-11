using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenuBtn
{
    public class MainMenu_PlayBtn : UIButton
    {
        public override void ClickEvent()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
