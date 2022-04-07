using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;

    #region Initializing section
    public void Init()
    {
        
    }

    public void Bind()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }
    #endregion

    public void LateUpdate()
    {
        transform.position = playerTransform.position + Offset;
    }
}


// Position 0.04, 2.29, -1.18
// Rotation 31, -1.937, 0
// Scale 1, 1, 1