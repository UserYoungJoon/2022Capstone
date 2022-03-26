using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Sound : MonoBehaviour
{
    public Slider sound_slider;

    private float prevSouncValue;

    public void OnMuteClieck(bool isOn)
    {
        if (isOn)
        {
            prevSouncValue = sound_slider.value;

            sound_slider.value = 0;
        }
        else
        {
            sound_slider.value = prevSouncValue;
        }
    }
}
