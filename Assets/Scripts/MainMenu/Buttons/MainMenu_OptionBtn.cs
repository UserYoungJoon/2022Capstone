using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenuBtn
{
    public class MainMenu_OptionBtn : UIButton
    {
        public GameObject OptionUI;

        public override void ClickEvent()
        {
            OpenUI(OptionUI);
        }
    }
}
