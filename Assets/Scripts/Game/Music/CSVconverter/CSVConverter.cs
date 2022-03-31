using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    참조: https://m.blog.naver.com/yoohee2018/220700239540
    수정사항: object를 prefab으로 변형, 음계/박자에따른 조건 세부화, x축 1/2/3번 Lane 랜덤화, 박자추가
 */
public class CSVConverter : MonoBehaviour
{
    public GameObject panel;    //prefab
    public Transform beatMapTransform;

    #region Initializing section
    public void Init()
    {
        int panelindex = 1;
        List<Dictionary<string, object>> data = CSVReader.Read("Airplane"); //data가 2차월 배열의 형태로 저장됨
        for (var i = 0; i < data.Count; i++)    //csv파일 읽기
        {
            if ((int)(data[i]["Speed"]) != 0)   //speed==0 은 노트의 종결을 의미
            {
                Debug.Log("Note: " + data[i]["Note"] + "Time: " + data[i]["Time"]);

                var obj = Instantiate(panel, new Vector3(0, 0, (int)data[i]["Time"] / 60), Quaternion.identity, beatMapTransform);
                obj.name = "panel" + panelindex;
                var child = obj.GetComponent<Transform>().Find("p");
                child.name = "p" + panelindex;
                panelindex++;
            }
        }
    }

    public void Bind()
    {
        
    }
    #endregion

}
