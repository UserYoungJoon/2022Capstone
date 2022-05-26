using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/*
    ????: https://m.blog.naver.com/yoohee2018/220700239540
    ????????: ????/????????? ???? ?????, ???????, prefeb?? ???
 */
public class CSVConverter
{
    public GameObject panel;    //prefab
    public Transform beatMap;   //parent obj
    public int[] arrayX = new int[3];

    public float correctionZ;   //z?? ????
    public static float mapDistance = 0;//no use yet
    public List<GameObject> panelList = new List<GameObject>();

    public List<string> panelsSideList = new List<string>();
    public List<Vector3> panelPositionList = new List<Vector3>();
    public List<GameObject> panelObjs = new List<GameObject>();
    private List<float> panelDistanceList = new List<float>();
    private List<Note> noteList = new List<Note>();
    private Notes notes;

    #region Initializing section
    public void Init()
    {
        arrayX[0] = -1;
        arrayX[1] = 0;
        arrayX[2] = 1;
    }

    public void Bind(GameObject panel, Transform beatMap, Notes notes)
    {
        this.panel = panel;
        this.beatMap = beatMap;
        this.notes = notes;
    }
    #endregion

    private const string TIME = "Time";
    private const string NOTE = "Note";
    
    public static float lastTime;
    public void MakeBeatMaps(string musicName)
    {
        mapDistance = 0;
        int panelindex = 1;
        List<Dictionary<string, object>> data = ReadCSV(musicName);

        int beforeTime = 0;
        GameObject panelObj = null;
        Vector3 newPanelPos;
        for (var i = 0; i < data.Count; i++)
        {
            if ((int)(data[i]["Speed"]) != 0)   //speed==0 
            {
                newPanelPos = new Vector3(GetRandomX(data, i), 0, correctionZ);
                panelObj = GameObject.Instantiate(panel, newPanelPos, Quaternion.identity, beatMap);
                panelObj.name = "panel" + panelindex;
                panelObjs.Add(panelObj);


                // decide side
                switch (panelObj.transform.position.x)
                {
                    case -1:
                        panelObj.tag = "Left";
                        break;
                    case 0:
                        panelObj.tag = "Center";
                        break;
                    case 1:
                        panelObj.tag = "Right";
                        break;
                }

                panelsSideList.Add(panelObj.tag);
                panelPositionList.Add(newPanelPos);
                panelList.Add(panelObj);

                var wayPoint = panelObj.transform.Find("p");
                wayPoint.name = "p" + panelindex;

                beforeTime = (int)data[i][TIME];

                panelindex++;
            }
            else
            {//Note setting to panelObj...
                if (panelObj == null)
                    break;

                int noteValue = (int)data[i][TIME] - beforeTime + 1;
                eNoteType noteType = eNoteType.NONE;
                if (noteValue == 30) noteType = eNoteType.HALFHALFNOTE;
                else if (noteValue == 60) noteType = eNoteType.HALFNOTE;
                else if (noteValue == 90) noteType = eNoteType.HALF_HH_NOTE;
                else if (noteValue == 120) noteType = eNoteType.ONE_NOTE;
                else if (noteValue == 240) noteType = eNoteType.TWO_NOTE;

                if (noteType == eNoteType.NONE)
                {//Note Setting failed Exception!
                    Debug.LogFormat("Note ERROR : Time[{0}] - Time[{1}] = {2} - {3} || Note value : {4}", i + 1, i, (int)data[i][TIME], beforeTime, noteValue);
                }

                //Note Obj Create & Set
                Note note = panelObj.transform.Find(NOTE).GetComponent<Note>();
                if (noteType == eNoteType.ONE_NOTE)
                    note.SetNote(noteType, notes.GetOneNoteByID((int)data[i][NOTE]));
                else
                    note.SetNote(noteType, notes.GetTwoNoteByID((int)data[i][NOTE]));
                noteList.Add(note);

                panelObj = null;
            }
            lastTime = (int)data[data.Count-2]["Time"]+1;
            
        }

        for (int i = 1; i < panelPositionList.Count; i++)
        {
            panelDistanceList.Add(Vector3.Distance(panelPositionList[i], panelPositionList[i - 1]));
            mapDistance += Vector3.Distance(panelPositionList[i], panelPositionList[i - 1]);
        }
    }

    public void Clear()
    {
        panelDistanceList.Clear();
        panelPositionList.Clear();
        panelObjs.Clear();
        noteList.Clear();
        panelList.Clear();
        panelsSideList.Clear();
    }

    private static int before = 0;
    private int GetRandomX(List<Dictionary<string, object>> data, int i)
    {
        int current = arrayX[Random.Range(0, 3)];
        // int z = ((int)data[i]["Time"] / 60) + 1;
        float tmp = ((int)data[i]["Time"] / 40.0f);
        float z = (tmp) + 1;
        if (Mathf.Abs(before - current) >= 2)
        {
            return GetRandomX(data, i);
        }
        else
        {
            if (before != current)
            {
                correctionZ = Mathf.Sqrt(Mathf.Pow(z, 2) - 1);
            }
            else
            {
                correctionZ = z;
            }
            before = current;
            return current;
        }
    }

    #region CSV Read
    private string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    private char[] TRIM_CHARS = { '\"' };

    private List<Dictionary<string, object>> ReadCSV(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
    #endregion CSV Read
}
