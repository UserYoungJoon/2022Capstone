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
        if (pos <= 0.04f && pos >= -0.04f)
        {
            Debug.Log("PERFECT");
            scoreType = eScoreType.PERFECT;
            resScore = 300;
        }
        else if (pos <= 0.08f && pos >= -0.08f)
        {
            Debug.Log("GREAT");
            scoreType = eScoreType.GREAT;
            resScore = 200;
        }
        else if (pos <= 0.12f && pos >= -0.12f)
        {
            Debug.Log("GOOD");
            scoreType = eScoreType.GOOD;
            resScore = 100;
        }
        else
        {
            Debug.Log("BAD");
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
