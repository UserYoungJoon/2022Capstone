using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    ����: https://m.blog.naver.com/yoohee2018/220700239540
    ��������: ����/���ڿ����� ���� ����ȭ, �����߰�, prefeb�� �߰�
 */
public class CSVConverter : MonoBehaviour
{
    public GameObject panel;    //prefab
    public Transform beatMapTransform;
    public int[] arrayX = new int[3];

    public float correctionZ;   //z�� ����

    static Vector3 newPanelPos;

    //���� 3������ CSV converter���� ������ ���� ������ ���� ������ �Űܰ���
    public static List<Vector3> panelPositionList = new List<Vector3>();
    public static List<float> panelDistanceList = new List<float>();
    public static List<Note> noteList = new List<Note>();

    #region Initializing section
    public void Init()
    {
        arrayX[0] = -1;
        arrayX[1] = 0;
        arrayX[2] = 1;

        MakeBeatMaps("AirPlane");
    }

    public void Bind()
    {

    }
    #endregion

    public void MakeBeatMaps(string musicName)
    {
        int panelindex = 1;
        List<Dictionary<string, object>> data = CSVReader.Read(musicName); //data�� 2���� �迭�� ���·� ������

        int beforeTime = 0;
        GameObject panelObj = null;
        for (var i = 0; i < data.Count; i++)    //csv���� �б�
        {
            if ((int)(data[i]["Speed"]) != 0)   //speed==0 �� ��Ʈ�� ������ �ǹ�
            {
                //Debug.Log("Note: " + data[i]["Note"] + "Time: " + data[i]["Time"]);
                newPanelPos = new Vector3(GetRandomX(data, i), 0, correctionZ);
                panelObj = Instantiate(panel, newPanelPos, Quaternion.identity, beatMapTransform);
                panelObj.name = "panel" + panelindex;
                panelPositionList.Add(newPanelPos);

                var wayPoint = panelObj.GetComponent<Transform>().Find("p");
                wayPoint.name = "p" + panelindex;

                beforeTime = (int)data[i]["Time"];

                panelindex++;
            }
            else
            {//Note setting section
                if (panelObj == null)
                    break;

                int noteValue = (int)data[i]["Time"] - beforeTime + 1;
                eNoteType noteType = eNoteType.NONE;
                if      (noteValue ==  30) noteType = eNoteType.HALFHALFNOTE;
                else if (noteValue ==  60) noteType = eNoteType.HALFNOTE;
                else if (noteValue ==  90) noteType = eNoteType.HALF_HH_NOTE;
                else if (noteValue == 120) noteType = eNoteType.ONE_NOTE;
                else if (noteValue == 240) noteType = eNoteType.TWO_NOTE;

                if (noteType == eNoteType.NONE)
                {
                    Debug.LogFormat("Note ERROE : Time[{0}] - Time[{1}] = {2} - {3} || Note valeue : {4}", i + 1, i, (int)data[i]["Time"],beforeTime,noteValue);
                }

                Note note = panelObj.transform.Find("Note").GetComponent<Note>();
                note.SetNote(noteType, (int)data[i]["Note"]);
                noteList.Add(note);

                panelObj = null;
            }
            NowPanelCount = panelindex;
        }

        for (int i = 1; i < panelPositionList.Count; i++)
        {
            panelDistanceList.Add(Vector3.Distance(panelPositionList[i], panelPositionList[i - 1]));
            //Debug.Log("Distance" + panelDistanceList[i - 1]);
        }
    }

    private static int before = 0;
    private int GetRandomX(List<Dictionary<string, object>> data, int i)
    {
        int current = arrayX[Random.Range(0, 3)];
        int z = ((int)data[i]["Time"] / 60) + 1;    //���� z�� ��

        if (Mathf.Abs(before - current) >= 2)
        {
            return GetRandomX(data, i);
        }
        else
        {
            if (before != current)
            {
                correctionZ = Mathf.Sqrt(Mathf.Pow(z, 2) - 1);   //z�� ����
                Debug.Log("correctionZ: " + correctionZ);
            }
            else
            {
                correctionZ = z;
            }
            before = current;
            //Debug.Log("current: " + current);
            return current;
        }
    }

    private int GetRandomx() // ���� �����⸦ �����ߴ� �Լ�
    {
        int nowRandom = 0;
        bool done = true;
        while(!done)
        {
            nowRandom = arrayX[Random.Range(0, 3)];
            if (Mathf.Abs(before - nowRandom) == 2)
            {
                continue;
            }
            else
            {
                done = true;
                before = nowRandom;
            }
        }
        return nowRandom;
    }
}
