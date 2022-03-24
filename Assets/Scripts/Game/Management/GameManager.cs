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

    void Awake()
    {
        stageCountText.text = "/ " + totalItemCount;
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }

    void OnTriggerEnter(Collider other) { 
        if(other.gameObject.tag == "Player")
            SceneManager.LoadScene(stage);
    }
}
