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
<<<<<<< Updated upstream

    public static AudioClip GetTwoNoteByID(int ID)
    {
        var res = twoNotes[getRealIndex(ID)];
=======
    const int OFFSET = 65;

    public static AudioClip GetTwoNoteByID(int ID)
    {
        var res = twoNotes[ID-OFFSET];
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        var res = oneNotes[getRealIndex(ID)];
=======
        var res = oneNotes[ID-OFFSET];
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

    private static int getRealIndex(int ID) //temp method
    {
        int res = -1;

        if (ID == 65)
            res = 0;
        else if (ID == 67)
            res = 1;
        else if (ID == 69)
            res = 2;

        return res;
    }
=======
>>>>>>> Stashed changes
}