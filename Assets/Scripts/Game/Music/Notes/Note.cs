using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eNoteType
{
    NONE,
    HALFHALFNOTE,
    HALFNOTE,
    HALF_HH_NOTE,
    ONE_NOTE,
    TWO_NOTE
}

public class Note : MonoBehaviour
{
    public eNoteType noteType = eNoteType.NONE;
    public AudioSource audioSource;

    public void SetNote(eNoteType noteType, AudioClip audio)
    {
        this.noteType = noteType;
        audioSource.clip = audio;
    }
}
