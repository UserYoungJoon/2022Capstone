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

    public float playTime = 2880 / 120 * 60 / 105; //2880은 csv파일에서 time의 마지막값+1으로 받아와야함

    
 
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
        for (int i = 1; i < CSVConverter.NowPanelCount; i++) // 수정필요
        {
            panelPosition = BeatMap.Find("panel" + i).position;
            panelPosition.y = 0f;
            points.Add(panelPosition);
        }
        
    }
    #endregion

    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.J))
        {
        //     Debug.Log(points[currentLocation + 1]);
        //     rigid.AddForce(new Vector3(0, 0, 50), ForceMode.Impulse);
        }
    }

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
            //transform.position = Vector3.MoveTowards(transform.position, points[currentLocation], Time.deltaTime * moveSpeed);
        }

        // 오브젝트 회전(x축)
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            Jumped();
            isJump = true;
            //panelPosition 간격에 따른 jumpPower를 수시로 수정해야됨

            // float distance = CSVConverter.panelDistanceList[currentLocation - 1];
            // float jumpAngle = 45;
            // var vel_y = Mathf.Sqrt(((-1)*(distance * (Physics.gravity.y-12))) / Mathf.Sin(2 * jumpAngle));
            // var vel = rigid.velocity;
            // vel.y = vel_y;
            // rigid.velocity = vel;


            //var velo = points[currentLocation] - playerPosition;
            Vector3 velo = 
                new Vector3(points[currentLocation].x - playerPosition.x, 0, 0) 
                + new Vector3(0, 0, points[currentLocation].z - playerPosition.z);
            Debug.Log(velo);
            rigid.velocity = velo;


            //rigid.AddForce(new Vector3(0, 50, 0), ForceMode.Impulse);
            rigid.AddForce(velo + new Vector3(0, 50, 0), ForceMode.Impulse);
            // Debug.Log("calEachPosition.z = " + points[currentLocation].z * 3.7f);
            // Vector3 fixedCalEachDistance = new Vector3(points[currentLocation].x * 3.7f, points[currentLocation].y, (points[currentLocation + 1].z + 10.0f));

            // if(points[currentLocation].x > 0)
            // {
            //     Debug.Log("오른쪽");
            //     fixedCalEachDistance.x += 10;   
            // }

            // else if(points[currentLocation].x < 0)
            // {
            //     Debug.Log("왼쪽");
            //     fixedCalEachDistance.x -= 10;
            // }

            // else if(points[currentLocation].x == 0)
            // {
            //     Debug.Log("가운데");
            //     fixedCalEachDistance.x = points[currentLocation].x;
            // }


            // rigid.AddForce(fixedCalEachDistance, ForceMode.Impulse);
            // // rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Item" && currentLocation == 1)
        {
            SoundManager.Instance.PlayBGMSound();
        }
        if (collision.gameObject.tag == "Item")
        {
            isJump = false;
            currentLocation++;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        // if(other.tag == "Way Point" && currentLocation == 1)
        // {
        //     SoundManager.Instance.PlayBGMSound();s
        // }
        if (other.tag == "Way Point")
        {
            SoundManager.Instance.getTime();
            currentLocation++;
        }

        //기본 공의 속도 계산
        float mapDistance = CSVConverter.mapDistance;
        Debug.Log("mapDistance: " + mapDistance);
        // Panel 12, Panel 13의 z 차는 4, -14 -18
        // if (points[currentLocation - 1].z - points[currentLocation].z <= -4.0f)
        // {
        //     jumpPower = 100; 
        //     moveSpeed = mapDistance / playTime;
        //     Debug.Log("Too far");
        // }
        // else if(points[currentLocation - 1].z - points[currentLocation].z >= -4.0f)
        // {
        //     jumpPower = 70;
        //     //moveSpeed = 2.5f;
        //     moveSpeed = mapDistance / playTime;
        //     Debug.Log("playTime: " + playTime);
        //     //Debug.Log("moveSpeed: "+moveSpeed);
        // }


        // else if (other.tag == "Finish")
        // {
        //     // 게임 성공 판단 여부는 GameManager.cs에 구현이 되있으므로 삭제함.
        // }
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
            //userScore += 300;
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




