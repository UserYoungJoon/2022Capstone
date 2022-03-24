using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Mainmenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickNewGame()
    {
        Debug.Log("New Game");
        //SceneManager.LoadScene("Scene1");
    }

    public void OnClickLoad()
    {
        Debug.Log("Load");
    }
    
    public void OnClickOption()
    {
        Debug.Log("Option");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
  
}
