using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    참조: https://m.blog.naver.com/yoohee2018/220700239540
    수정사항: 음계/박자에따른 조건 세부화, 박자추가, prefeb음 추가
 */
public class CSVConverter : MonoBehaviour
{
    public GameObject panel;    //prefab
    public Transform beatMapTransform;
    public int[] arrayX = new int[3];

    static Vector3 newPanelPos;

    //다음 3가지는 CSV converter에서 가지고 있을 이유가 없음 조만간 옮겨갈것
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
        List<Dictionary<string, object>> data = CSVReader.Read(musicName); //data가 2차월 배열의 형태로 저장됨

        int beforeTime = 0;
        GameObject panelObj = null;
        for (var i = 0; i < data.Count; i++)    //csv파일 읽기
        {
            if ((int)(data[i]["Speed"]) != 0)   //speed==0 은 노트의 종결을 의미
            {
                //Debug.Log("Note: " + data[i]["Note"] + "Time: " + data[i]["Time"]);
                newPanelPos = new Vector3(GetRandomX(), 0, ((int)data[i]["Time"] / 60) + 1);
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
                if      (noteValue == 15) noteType = eNoteType.HALFHALFNOTE;
                else if (noteValue == 30) noteType = eNoteType.HALFNOTE;
                else if (noteValue == 60) noteType = eNoteType.ONE_NOTE;
                else if (noteValue == 90) noteType = eNoteType.TWO_NOTE;

                if (noteType == eNoteType.NONE)
                {
                    Debug.LogFormat("Note ERROE : Time[{0}] - Time[{1}] = {2} - {3} || Note valeue : {4}", i + 1, i, (int)data[i]["Time"],beforeTime,noteValue);
                }

                Note note = panelObj.transform.Find("Note").GetComponent<Note>();
                note.SetNote(noteType, (int)data[i]["Note"]);
                noteList.Add(note);

                panelObj = null;
            }
        }

        for (int i = 1; i < panelPositionList.Count; i++)
        {
            panelDistanceList.Add(Vector3.Distance(panelPositionList[i], panelPositionList[i - 1]));
            Debug.Log("Distance" + panelDistanceList[i - 1]);
        }
    }

    private static int before = 0;
    private int GetRandomX()
    {
        int current = arrayX[Random.Range(0, 3)];

        if (Mathf.Abs(before - current) >= 2)
        {
            return GetRandomX();
        }
        else
        {
            before = current;
            //Debug.Log("current: " + current);
            return current;
        }
    }

    private int GetRandomx() // 제가 만들기를 희망했던 함수
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
