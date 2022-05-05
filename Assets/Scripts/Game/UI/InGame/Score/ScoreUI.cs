using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public List<Sprite> sprites = new List<Sprite>();
    public Animator animator;
    public Image image;
    public TMP_Text score;

    private int nowScore = 0;
    private Dictionary<eScoreType, Sprite> scoreEffectSprites = new Dictionary<eScoreType, Sprite>(4);
    private const string hit = "Hit";
    
    
    public void Awake()
    {
        scoreEffectSprites.Add(eScoreType.BAD, sprites[0]);
        scoreEffectSprites.Add(eScoreType.GOOD, sprites[1]);
        scoreEffectSprites.Add(eScoreType.GREAT, sprites[2]);
        scoreEffectSprites.Add(eScoreType.PERFECT, sprites[3]);
    }

    public void SetBeforeStart()
    {
        nowScore = 0;
        score.text = nowScore.ToString();
    }

    public void UpdateUI(int addScore, eScoreType scoreType)
    {
        nowScore += addScore;
        score.text = nowScore.ToString();

        image.sprite = scoreEffectSprites[scoreType];
        animator.SetTrigger(hit);
    }
}
