using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum eScoreType
{
    NONE,
    PERFECT,
    GREAT,
    GOOD,
    BAD
}


public class ScoreUI : MonoBehaviour
{
    public TMP_Text score;

    private int nowScore = 0;

    // expectint to read [Resources.LoadAll<GameObject>("Effect")]...
    private Dictionary<eScoreType, GameObject> scoreEffects = new Dictionary<eScoreType, GameObject>(4);

    public void SetBeforeStart()
    {
        nowScore = 0;
        score.text = nowScore.ToString();
    }

    public void UpdateUI(int addScore,eScoreType scoreType)
    {
        nowScore += addScore;
        score.text = nowScore.ToString();

        //Effect 발생 코드 미구현
        //scoreEffects[scoreType].SetActive(true);
    }
}
