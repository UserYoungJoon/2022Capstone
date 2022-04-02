using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Notes
{
    private static List<AudioClip> oneNotes;
    private static List<AudioClip> twoNotes;

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

    public static AudioClip GetTwoNoteByID(int ID)
    {
        var res = twoNotes[ID-OFFSET];
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

    public static AudioClip GetOneNoteByID(int ID)
    {
       // Debug.LogFormat("cnt : {0} and [ID-offset:{1}],[ID:{2}],", twoNotes.Count, ID - 65,ID);
        var res = oneNotes[ID-OFFSET];
        if (res != null)
        {
            return res;
        }
        else
        {
            Debug.LogFormat("Can't Find Note ID:{0} from [Notes]", ID);
            return null;
        }
    }
}