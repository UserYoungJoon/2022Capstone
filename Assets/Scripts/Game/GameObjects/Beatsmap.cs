using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatsmap : MonoBehaviour
{
    private List<GameObject> panelObjs = new List<GameObject>();

    private int panel_Index = 0;

    public void Bind(List<GameObject> _panelObjs)
    {
        panelObjs = _panelObjs;
    }

    public void SetBeforeRun()
    {
        for (int index = 0; index < 5; index++)
        {
            panelObjs[index].SetActive(true);
        }

        for (int index = 5; index < transform.childCount; index++)
        {
            panelObjs[index].SetActive(false);
        }
        panel_Index = 4;
    }
    public void Clear()
    {
        foreach(var panelObj in panelObjs)
        {
            DestroyImmediate(panelObj);
        }
    }

    public void ShowNextBlock()
    {
        if ((panel_Index + 1) < panelObjs.Count)
        {
            panel_Index++;
            panelObjs[panel_Index].SetActive(true);
        }
    }
}
