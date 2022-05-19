using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuBtn
{
    public class MainMenu_BackBtn : UIButton
    {

        public GameObject OptionUI;
        public Slider slider;

        public override void ClickEvent()
        {
            SoundManager.Instance.SetVolume(slider.value);
            Debug.Log("Set " + slider.value);
            CloseUI(OptionUI);
        }
    }
}