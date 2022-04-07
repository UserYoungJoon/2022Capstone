using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public AudioSource audio;
    private void Awake()
    {
    }

    private void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
    }
    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.tag == "Player")
        {
          audio.Play();
        }

    }
}
