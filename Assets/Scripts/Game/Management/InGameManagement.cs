using System.Collections;
using System.Collections.Generic;
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
    }
}
