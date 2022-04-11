using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private enum eCameraMode
    {
        SELCET_SONG,
        GAME
    }
    private eCameraMode cameraMode;
    private Transform playerTransform;
    private Vector3 Offset;

    #region Initializing section
    public void Init()
    {
        cameraMode = eCameraMode.SELCET_SONG;
    }

    public void Bind(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        Offset = transform.position - playerTransform.position;
    }
    #endregion


    public void SetSelectMode()
    {
        cameraMode = eCameraMode.SELCET_SONG;
    }

    public void SetGameMode()
    {
        cameraMode = eCameraMode.GAME;
    }


    public void LateUpdate()
    {
        if(cameraMode == eCameraMode.GAME)
            transform.position = playerTransform.position + Offset;
    }
}


// Position 0.04, 2.29, -1.18
// Rotation 31, -1.937, 0
// Scale 1, 1, 1