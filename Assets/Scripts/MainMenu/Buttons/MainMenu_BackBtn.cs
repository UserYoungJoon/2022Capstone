using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenuBtn
{
    public class MainMenu_BackBtn : UIButton
    {
        public GameObject OptionUI;

        public override void ClickEvent()
        {
            CloseUI(OptionUI);
        }
    }
}