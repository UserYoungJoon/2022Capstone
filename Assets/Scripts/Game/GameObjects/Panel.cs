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
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == TagType.PLAYER)
        {
          audioSource.Play(0);
        }
        beatsmap.ShowNextBlock();
    }
}