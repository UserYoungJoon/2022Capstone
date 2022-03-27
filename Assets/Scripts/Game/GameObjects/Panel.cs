using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Player")
        {
            audio.Play();

        }
    }
}
