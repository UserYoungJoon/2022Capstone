using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseBtnType : MonoBehaviour
{
    public BTNType currentType;
    // Start is called before the first frame update
    public CanvasGroup pauseGroup;
    public CanvasGroup inGameGroup;

    public void PauseOnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Pause:
                Debug.Log("일시정지");
                CanvasGroupOn(pauseGroup);
                CanvasGroupOff(inGameGroup);
                break;
            case BTNType.LoadGame:
                Debug.Log("일시정지");
                CanvasGroupOn(pauseGroup);
                CanvasGroupOff(inGameGroup);
                break;
        }
    }
        public void CanvasGroupOn(CanvasGroup cg)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
        public void CanvasGroupOff(CanvasGroup cg)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

}
