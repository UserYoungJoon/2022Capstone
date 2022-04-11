using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public GameManager manager;
    public Transform startPos;
    public Transform BeatMap;

    private float rotateSpeed = 300.0f;  //회전속도
    private int currentLocation;
    private Rigidbody rigid;
    private bool isJumping;

    #region Initializing section
    public void Init()
    {
        isJumping = false;
        rigid = GetComponent<Rigidbody>();
        points.Add(this.gameObject.transform.position);
        currentLocation = 0;
    }

    public void Bind(List<Vector3> pts)
    {
        points = pts;
    }
    #endregion

    public void SetBeforeRun()
    {
        transform.position = new Vector3(startPos.position.x, 0.5f, startPos.position.z);
        nowPos = startPos.position;
        nextPos = points[0];
        currentLocation = 0;
        isJumping = false;
    }

    void Update()
    {
        Move();
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }
    }

    #region Move control section
    private float deltaTime = 1;
    private float forwardSpeed = 1.8f;
    private float halfPanelSize = 0.12f;
    private Vector3 nowPos;
    private Vector3 nextPos;
    void Jump()
    {
        deltaTime = (nextPos.z - nowPos.z - halfPanelSize) / forwardSpeed;
        Vector3 vector = rigid.velocity;
        vector.x = (nextPos.x - transform.position.x) / deltaTime;
        vector.y = -Physics.gravity.y * deltaTime / 2;
        vector.z = forwardSpeed;
        rigid.velocity = vector;

        isJumping = true;
        manager.CalculateScore((transform.position - nowPos).z);
    }
    void Move()
    {
        deltaTime = (nextPos.z - nowPos.z - halfPanelSize) / forwardSpeed;
        Vector3 vector = rigid.velocity;
        vector.z = forwardSpeed;
        rigid.velocity = vector;
    }
    #endregion Move control section

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Way Point" && currentLocation == 0)
        {
            SoundManager.Instance.PlayBGMSound();
        }
        if (other.tag == "Way Point")
        {
            nowPos = points[currentLocation];
            nextPos = points[currentLocation + 1];
            currentLocation++;
        }
    }
}

// 플레이어의 위치와 포인트의 거리에 따른 점수 계산
// 0.02 0.03 0.05
// if playerPosition = point or playerPosition - point >= |3|
//   then Perfect
// if playerPosition - point >= |5|
//   then Great
// if playerPosition - point >= |7|
//   then good
// if playerPosition - point >= |10|
//   then bad


//              Algorithm B
// 패널의 색변화에 따라 점수판정

// if playerStatus = collisionToPanel
//   then PanelStartedChangeColour

// load PanelChaningColour()
// update(getPanelColour)
// time = getColourChangeTime()

// if time > 1.0
//   then Perfect()
// if time > 2.0
//   then Great()
// if time > 3.0
//   then Good()
// if time > 4.0
//   then Bad()