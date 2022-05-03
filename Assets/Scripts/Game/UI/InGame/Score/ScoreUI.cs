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
    private Dictionary<eScoreType, Sprite> scoreEffectSprites = new Dictionary<eScoreType, Sprite>(4);
    public List<Sprite> sprites = new List<Sprite>();
    public void Awake()
    {
        scoreEffectSprites.Add(eScoreType.BAD, sprites[0]);
        scoreEffectSprites.Add(eScoreType.GOOD, sprites[1]);
        scoreEffectSprites.Add(eScoreType.GREAT, sprites[2]);
        scoreEffectSprites.Add(eScoreType.PERFECT, sprites[3]);

        sprites = null;
    }

    // expectint to read [Resources.LoadAll<GameObject>("Effect")]...

    public void SetBeforeStart()
    {
        nowScore = 0;
        score.text = nowScore.ToString();
    }

    public Animator effect;
    public void UpdateUI(int addScore, eScoreType scoreType)
    {
        nowScore += addScore;
        score.text = nowScore.ToString();

        //effect.sprite = scoreEffectSprites[scoreType];
        //effect.Open();
    }
}
