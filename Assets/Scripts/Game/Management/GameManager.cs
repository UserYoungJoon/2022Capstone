using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public Text playerCountText;
    private bool gameOverBoolean = false;
    private int userScore = 0;

    void Awake()
    {
        
    }

    public void ScoreCal()
    {
        playerCountText.text = userScore.ToString();
    }


    public void GetPerfect()
    {
        Debug.Log("Perfect!");
        userScore += 300;
        ScoreCal();
    }

    public void GetGreat()
    {
        Debug.Log("Great!");
        userScore += 200;
        ScoreCal();
    }

    public void GetGood()
    {
        Debug.Log("Good!");
        userScore += 100;
        ScoreCal();
    }

    public void GetBad()
    {
        Debug.Log("Bad!");
        userScore += 50;
        ScoreCal();
    }


    public bool GetGameOverBoolean()
    {
        return gameOverBoolean;
    }

    void OnTriggerEnter(Collider other) 
    { 
        if(other.gameObject.tag == "Player")
        {
            gameOverBoolean = true;
            if(gameOverBoolean == true)
                Debug.Log("GAME OVER");
                Debug.Log("Final Score is: " + userScore);
            
            SceneManager.LoadScene(stage);

        }
    }
}
