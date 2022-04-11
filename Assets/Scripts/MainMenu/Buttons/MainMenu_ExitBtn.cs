using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenuBtn
{
    public class MainMenu_ExitBtn : UIButton
    {
        public override void ClickEvent()
        {
            Application.Quit();
        }
    }
}

