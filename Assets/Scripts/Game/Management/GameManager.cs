using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public Text stageCountText;
    public Text playerCountText;
    private bool gameOverBoolean = false;
    private int userScore = 0;

    void Awake()
    {
        stageCountText.text = "/ " + totalItemCount;
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }

    public void GetPerfect()
    {
        Debug.Log("Perfect!");
        userScore += 300;
    }

    public void GetGreat()
    {
        Debug.Log("Great!");
        userScore += 200;
    }

    public void GetGood()
    {
        Debug.Log("Good!");
        userScore += 100;
    }

    public void GetBad()
    {
        Debug.Log("Bad!");
        userScore += 50;
    }


    public bool GetGameOverBoolean(){
        return gameOverBoolean;
    }

    void OnTriggerEnter(Collider other) { 
        if(other.gameObject.tag == "Player")
        {
            gameOverBoolean = true;
            if(gameOverBoolean == true)
                Debug.Log("GAME OVER");
            
            SceneManager.LoadScene(stage);

        }
    }
}
