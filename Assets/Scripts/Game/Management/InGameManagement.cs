using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class GameManager
{

    // Calculate score int playing game. 
    // Called by Jump.
    public void CalculateScore(float pos)
    {
        int resScore = 0;
        eScoreType scoreType = eScoreType.NONE;
        if (pos <= 0.05f)
        {
            //Debug.Log("PERFECT");
            scoreType = eScoreType.PERFECT;
            resScore = 300;
            
        }
        else if (pos <= 0.1f)
        {
            //Debug.Log("GREAT");
            scoreType = eScoreType.GREAT;
            resScore = 200;
        }
        else if (pos <= 0.3f)
        {
            //Debug.Log("GOOD");
            scoreType = eScoreType.GOOD;
            resScore = 100;
        }
        else if(pos <= 0.5f)
        {
            //Debug.Log("BAD");
            scoreType = eScoreType.BAD;
            resScore = 50;
        }

        //ó���� ������ ���� Event
        userScore += resScore;
        uIManager.UpdateScoreUI(resScore, scoreType);
    }

    public void FinishGame(bool isClearedGame)
    {
        SoundManager.Instance.StopSongSound();
        uIManager.OpenFinishGameUI(userScore, isClearedGame, null);
        SaveScoreToJson();
    }

    Scoretable scoretable;
    public void InitScoreFile()
    {
        if(File.Exists(Application.dataPath + "/TestJson.json"))
        {
            string str = File.ReadAllText(Application.dataPath + "/TestJson.json");
            
            scoretable = JsonUtility.FromJson<Scoretable>(JsonUtility.ToJson(str));

            Debug.Log("Successfully load json file: " + str);
            return;
        }

        else 
        {
            scoretable = new Scoretable();
            scoretable.name = "Default";
            scoretable.AddPlayerScore(0);
            string str = JsonUtility.ToJson(scoretable);
            Debug.Log("ToJson : " + str);

            File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(scoretable));
            Debug.Log("New json file saved in your directory.");
        }
    }
    public void SaveScoreToJson()
    {
        scoretable.AddPlayerScore(userScore);
        string str = JsonUtility.ToJson(scoretable);
            File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(scoretable));
        Debug.Log(str);
    }
}

class Scoretable {
    public string name;
    public List<int> score = new List<int>();

    public void printScore()
    {
        Debug.Log("Player " + name +" got " + score + "!");
    }

    public void AddPlayerScore(int userScore)
    {
        score.Add(userScore);
    }
}
