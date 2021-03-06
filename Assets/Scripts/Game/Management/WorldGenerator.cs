using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{//World Generate 
    private List<GameObject> streets;
    private List<GameObject> shortStreets;
    private List<GameObject> goals;

    public Transform environmentIndexer; // not binding yet
    public Goal goal;
    public Floor floor;
    public void InitWorldGenerator()
    {
        streets         = new List<GameObject>();
        shortStreets    = new List<GameObject>();
        goals           = new List<GameObject>();

        foreach (var obj in Resources.LoadAll<GameObject>("Environment/Street"))
            streets.Add(obj);
        foreach (var obj in Resources.LoadAll<GameObject>("Environment/ShortStreet"))
            shortStreets.Add(obj);
        foreach (var obj in Resources.LoadAll<GameObject>("Environment/Goal"))
            goals.Add(obj);

    }

    const string AIRPLANE = "Airplane";//temp code pls modify to make dynamically
    public void SetBeforeGenerate()
    {
        ClearWorld();
        CSVConverter.MakeBeatMaps(AIRPLANE);
        floor.SetBeforeRun();
    }

    private bool isCleared = true;
    public void ClearWorld()
    {
        if (!isCleared)
        {
            for(int i = 1; i <= CSVConverter.wayPointsList.Count;i++)
            {
                var panel = beatMap.Find("panel" +i ).gameObject;
                Destroy(panel);
            }
            CSVConverter.Clear();
            Timer.MeltTime();
            isCleared = true;
        }
    }

    public void GenerateWorld()
    {
        int wholeStreetLen = (int)goal.transform.position.z;

        // 1. Generate street to goals
        const int START_STREET_POS = 10;
        float nowStreetLen = START_STREET_POS;
        while ((wholeStreetLen - nowStreetLen) > 25)// until [Street.Length > 남은 길이]일 때까지
        {
            GameObject nowStreet = Instantiate(streets[Random.Range(0, streets.Count)], environmentIndexer);
            var pos = nowStreet.transform.position;
            nowStreet.transform.position = new Vector3(pos.x, pos.y, nowStreetLen);
            nowStreetLen += 25;
        }

        nowStreetLen -= 12.5f;
        // 2. Generate shortStreet to goals
        while ((wholeStreetLen - nowStreetLen) > -30)
        {
            GameObject nowStreet = Instantiate(shortStreets[Random.Range(0, shortStreets.Count)], environmentIndexer);
            var pos = nowStreet.transform.position;
            nowStreet.transform.position = new Vector3(pos.x, pos.y, nowStreetLen);
            nowStreetLen += 12.5f;
        }


        // 3. Generate Goal
        //   - 

        // 추가 목표 : 바람에 따라 떨어지는 나뭇잎?
        // 지혼자 돌아다니는 새?

        isCleared = false;
    }
}

