using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eNoteType
{
    NONE,
    HALFHALFNOTE,
    HALFNOTE,
    ONE_NOTE,
    TWO_NOTE
}

public class Note : MonoBehaviour
{
    public eNoteType noteType = eNoteType.NONE;
    public AudioSource audioSource;

    public void SetNote(eNoteType noteType, int noteID)
    {
        this.noteType = noteType;

        if (noteType == eNoteType.TWO_NOTE)
            audioSource.clip = Notes.GetTwoNoteByID(noteID);
        else
            audioSource.clip = Notes.GetOneNoteByID(noteID);
    }
}
