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
    public Beatsmap beatsmap;
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

    public string SELECT_SONG; // will be dynamic variable
    public void SetSelectSong(string selectedSong)
    {
        this.SELECT_SONG = selectedSong;
    }
    public void SetBeforeGenerate()
    {
        ClearWorld();
        CSVConverter.MakeBeatMaps(SELECT_SONG);
        floor.SetBeforeRun();
        beatsmap.SetBeforeRun();
    }

    private bool isCleared = true;
    public void ClearWorld()
    {
        if (!isCleared)
        {
            beatsmap.Clear();
            CSVConverter.Clear();
            Timer.MeltTime();
            isCleared = true;
        }
    }

    public void GenerateWorld()
    {
        // 0. Generate Goal
        Vector3 lastPanelPosition = CSVConverter.panelPositionList[CSVConverter.panelPositionList.Count - 1];
        Vector3 goalPosition = new Vector3(0, 0, lastPanelPosition.z + 10f);
        goal.transform.position = goalPosition;
        

        int wholeStreetLen = (int)goal.transform.position.z;

        // 1. Generate street to goals
        const int START_STREET_POS = 0;
        float nowStreetLen = START_STREET_POS;
        while ((wholeStreetLen - nowStreetLen) > -30)// until [Street.Length > ... ]
        {
            GameObject nowStreet = Instantiate(streets[Random.Range(0, streets.Count)], environmentIndexer);
            var pos = nowStreet.transform.position;
            nowStreet.transform.position = new Vector3(pos.x, pos.y, nowStreetLen);
            nowStreetLen += 10;
        }

        nowStreetLen -= 12.5f;
        // // 2. Generate shortStreet to goals
        // while ((wholeStreetLen - nowStreetLen) > -30)
        // {
        //     GameObject nowStreet = Instantiate(shortStreets[Random.Range(0, shortStreets.Count)], environmentIndexer);
        //     var pos = nowStreet.transform.position;
        //     nowStreet.transform.position = new Vector3(pos.x, pos.y, nowStreetLen);
        //     nowStreetLen += 12.5f;
        // }


        

        isCleared = false;
    }
}

