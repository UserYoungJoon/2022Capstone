using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public AudioSource audioSource;

    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.tag == "Player")
        {
          audioSource.Play();
        }
    }
}