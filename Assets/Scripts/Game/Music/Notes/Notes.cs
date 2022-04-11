using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Notes
{
    private List<AudioClip> oneNotes;
    private List<AudioClip> twoNotes;

    #region
    public void Init()
    {
        oneNotes = new List<AudioClip>();
        twoNotes = new List<AudioClip>();

        foreach (var no in Resources.LoadAll("Notes1"))
        {
            oneNotes.Add(no as AudioClip);
        }

        foreach (var no in Resources.LoadAll("Notes2"))
        {
            twoNotes.Add(no as AudioClip);
        }
    }

    public void Bind()
    {
        
    }
    #endregion
    const int OFFSET = 65;

    public AudioClip GetTwoNoteByID(int ID)
    {
        return Gett(twoNotes,ID);
    }

    public AudioClip GetOneNoteByID(int ID)
    {
        return Gett(oneNotes, ID);
    }

    public AudioClip Gett(List<AudioClip> list, int ID)
    {
        var res = list[ID - OFFSET];
        if (res != null)
        {
            return res;
        }
        else
        {
            Debug.LogFormat("Can't Find Note ID:{0} from [TwoNotes]", ID);
            return null;
        }
    }
}