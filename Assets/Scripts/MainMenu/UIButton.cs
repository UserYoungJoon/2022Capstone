using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 buttonScale;
    private Vector3 defaultScale;
    //public CanvasGroup mainGroup;
    //public CanvasGroup optionGroup;
    //public CanvasGroup songGroup;
    //public CanvasGroup playGroup;
    //public CanvasGroup1 songGroup1;
    //public CanvasGroup1 playGroup1;

    public virtual void Init()
    {
        buttonScale = GetComponent<Transform>().localScale;
        defaultScale = buttonScale;
    }

    public void OpenUI(GameObject ui)
    {
        ui.SetActive(true);
        //cg.alpha = 1;
        //cg.interactable = true;
        //cg.blocksRaycasts = true;
    }
    public void CloseUI(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale = defaultScale;
    }

    public virtual void ClickEvent()
    {
        Debug.Log("No action from button : [" + name + "]");
    }
}
