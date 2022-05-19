using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class OptionUi_Slider : MonoBehaviour
{
    //public static float volume = 1f;
    // public Slider slider;
    // private void Start()
    // {
    //     Debug.Log("StartFrom: " + gameObject.name);
    //     slider = gameObject.GetComponent<Slider>();

    //     slider.value = volume;


    // }
    // public void SliderUpdate()
    // {
    //     //SoundManager.Instance.
    //     volume = slider.value;
    //     //SoundManager.Instance.SetVolume(volume);
    // }
    public Slider sound_slider;
    private void Awake()
    {
        sound_slider.value = SoundManager.Instance.volume;
    }
    public float GetSliderValue()
    {
        return sound_slider.value;
    }
}
