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
    static int before = 0;
    static Vector3 tmpVector;
    static List<Vector3> panelPosition = new List<Vector3>();
    static List<float> panelDistance = new List<float>();

    #region Initializing section

    private int RandomFunction()
    {
        int current = arrayX[Random.Range(0, 3)];

        if (Mathf.Abs(before - current) >= 2)
        {
            return RandomFunction();
        }
        else
        {    
            before = current;
            //Debug.Log("current: " + current);
            return current;   
        }
    }

    public void Init()
    {
        arrayX[0] = -1;
        arrayX[1] = 0;
        arrayX[2] = 1;
        int panelindex = 1;
        List<Dictionary<string, object>> data = CSVReader.Read("Airplane"); //data가 2차월 배열의 형태로 저장됨
        for (var i = 0; i < data.Count; i++)    //csv파일 읽기
        {
            if ((int)(data[i]["Speed"]) != 0)   //speed==0 은 노트의 종결을 의미
            {
                //Debug.Log("Note: " + data[i]["Note"] + "Time: " + data[i]["Time"]);

                tmpVector = new Vector3(RandomFunction(), 0, ((int)data[i]["Time"] / 60) + 1);
                panelPosition.Add(tmpVector);
                var obj = Instantiate(panel, tmpVector, Quaternion.identity, beatMapTransform);
                //Debug.Log("Position" + tmpVector);

                obj.name = "panel" + panelindex;
                var child = obj.GetComponent<Transform>().Find("p");
                child.name = "p" + panelindex;
                panelindex++;
            }
        }
        for (var i = 1; i < panelPosition.Count; i++)
        {
            panelDistance.Add(Vector3.Distance(panelPosition[i], panelPosition[i - 1]));
            Debug.Log("Distance" + panelDistance[i - 1]);
        }
    }

    public void Bind()
    {
        
    }
    #endregion

}
