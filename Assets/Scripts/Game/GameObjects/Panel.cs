using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public AudioSource audioSource;
    public static Beatsmap beatsmap;

    private void Start()
    {
        beatsmap = GameObject.Find("Beatsmap").GetComponent<Beatsmap>();
        transform.GetChild(1).GetComponent<AudioSource>().volume = SoundManager.Instance.volume;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagType.PLAYER)
        {
            //Debug.Log("Note Volume: " + transform.GetChild(1).GetComponent<AudioSource>().volume);
            audioSource.Play();
        }
        beatsmap.ShowNextBlock();
    }
}


// Scale 0.8 0.05 0.8