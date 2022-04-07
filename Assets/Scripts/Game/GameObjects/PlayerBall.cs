using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    [SerializeField]
    public float jumpPower;
    public GameManager manager;
    public static bool isJump;
    Rigidbody rigid;
    public List<Vector3> points = new List<Vector3>();
    private int currentLocation = 0;
    bool hasKeyDown = false;
    private bool startedGame = false;
    private Vector3 playerPosition;
    private Vector3 calEachPosition;

    public float moveSpeed;    // 이동속도(z축)
    private float rotateSpeed = 300.0f;  //회전속도
    public Transform BeatMap;

    #region Initializing section
    public void Init()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        points.Add(this.gameObject.transform.position);
    }

    public void Bind()
    {
        Vector3 panelPosition;
        for (int i = 1; i <= CSVConverter.NowPanelCount; i++) // 수정필요
        {
            panelPosition = BeatMap.Find("panel" + i).Find("p" + i).position;
            panelPosition.y = 0f;
            points.Add(panelPosition);
        }
    }
    #endregion


    void Update()
    {
        // Game start
        if (Input.GetKeyDown(KeyCode.J) && hasKeyDown == false)
        {
            hasKeyDown = true;
            startedGame = true;
            currentLocation++;
        }
        if (hasKeyDown == true)
        {
            
            // Debug.Log("Moving to: " + points[currentLocation].ToString());
            transform.position = Vector3.MoveTowards(transform.position, points[currentLocation], Time.deltaTime * moveSpeed);
        }

        // 오브젝트 회전(x축)
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            Jumped();
            isJump = true;
            //panelPosition 간격에 따른 jumpPower를 수시로 수정해야됨
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Way Point" && currentLocation == 1)
        {
            SoundManager.Instance.PlayBGMSound();
        }
        if (other.tag == "Way Point")
        {
            currentLocation++;
        }

        
        // Panel 12, Panel 13의 z 차는 4, -14 -18
        if(points[currentLocation - 1].z - points[currentLocation].z <= -4.0f)
        {
            jumpPower = 100;
            moveSpeed = 5.0f;
            Debug.Log("Too far");
        }
        else if(points[currentLocation - 1].z - points[currentLocation].z >= -4.0f)
        {
            jumpPower = 50;
            moveSpeed = 2.5f;
        }


        else if (other.tag == "Finish")
        {
            // 게임 성공 판단 여부는 GameManager.cs에 구현이 되있으므로 삭제함.
        }
    }


    // When player jumped
    private void Jumped()
    {
        playerPosition = transform.position;
        calEachPosition = (playerPosition - points[currentLocation - 1]);
       // Debug.Log(currentLocation + ", Calculated: " + calEachPosition.z);


        // 점수 계산 테스트
        // -0.1 < z < 0.1
        if (calEachPosition.z <= 0.04f && calEachPosition.z >= -0.04f)
        {
            Debug.Log("Perfect!");
            userScore += 300;
            manager.ScoreCal();
        }
        else if (calEachPosition.z <= 0.08f && calEachPosition.z >= -0.08f)
        {
            manager.GetGreat();
        }
        else if (calEachPosition.z <= 0.12f && calEachPosition.z >= -0.12f)
        {
            manager.GetGood();
        }
        else
        {
            manager.GetBad();
        }

    }
}



//             Algorithm A
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




