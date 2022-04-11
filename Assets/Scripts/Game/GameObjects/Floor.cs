using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject goal;

    private BoxCollider boxCollider;

    public void Init()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void SetBeforeRun()
    {
        int goalPosZ = (int)goal.transform.position.z;
        var size = boxCollider.size;
        size = new Vector3(6, 0.2f, goalPosZ + 10);
        boxCollider.size = size;

        var pos = transform.position;
        pos.z = goalPosZ / 2;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Game over : Floor");
        gameManager.FinishGame(false);
    }
}
