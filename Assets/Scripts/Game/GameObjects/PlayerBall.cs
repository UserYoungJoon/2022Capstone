using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    public GameManager manager;
    bool isJump;
    Rigidbody rigid;
    public List<Vector3> points = new List<Vector3>();
    private int currentLocation = 0;
    bool hasKeyDown = false;
    private bool startedGame = false;

    public float moveSpeed;    //이동속도(z축)
    private float rotateSpeed = 300.0f;  //회전속도

    private void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        GameObject panel;
        Transform panelTransform;
        Vector3 panelPosition;

        // 초기 공의 위치
        points.Add(this.gameObject.transform.position);

        // 생성된 패널의 수를 리스트에 추가, i <= panel
        for (int i = 1; i <= 27; i++)
        {
            panel = GameObject.Find("p" + i.ToString());
            panelTransform = panel.transform;
            panelPosition = panelTransform.position;
            panelPosition.y = 0;
            points.Add(panelPosition);
        }
    }
    // J키 입력 시 출발 (임시)
    private void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.J) && !startedGame)
        {
            currentLocation++;
            startedGame = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && hasKeyDown == false)
        {
            hasKeyDown = true;
        }
        if (hasKeyDown == true)
        {
            checkInput();
            // Debug.Log("Moving to: " + points[currentLocation].ToString());
            transform.position = Vector3.MoveTowards(transform.position, points[currentLocation], Time.deltaTime * 3);
        }
        // 디버깅용 패널 위치 확인하기
        if(Input.GetKeyDown(KeyCode.B)){
            Debug.Log("Current list number is: " + currentLocation);
        }

        //오브젝트 회전(x축)
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;

        if (collision.gameObject.tag == "Item")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Way Point")
        {
            currentLocation++;
        }
        
        if (other.tag == "Finish")
        {
            if (itemCount == manager.totalItemCount)
            {
                //Game Clear!
                if (manager.stage == 1)
                    SceneManager.LoadScene(0);
                else
                    SceneManager.LoadScene(manager.stage + 1);
            }
            else
            {
                //Restart..
                SceneManager.LoadScene(manager.stage);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Way Point")
        {   
            // 점수 계산 테스트
            manager.GetPerfect();
            Debug.Log("You are Leaving at: " + currentLocation);
        }
        
    }
}



//             Algorithm A
// 플레이어의 위치와 포인트의 거리에 따른 점수계산

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

            


