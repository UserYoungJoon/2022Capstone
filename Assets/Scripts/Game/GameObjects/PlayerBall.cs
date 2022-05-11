using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public List<string> sides = new List<string>();
    public List<GameObject> panelList = new List<GameObject>();
    public GameManager manager;
    public Transform startPos;
    public Transform BeatMap;

    private float rotateSpeed = 300.0f;  //회전속도
    private int currentLocation;
    private Rigidbody rigid;
    private bool isJumping;
    public static bool inputTiming;
    // !! TEST !!
    public GameObject testImage;

    #region Initializing section
    public void Init()
    {
        isJumping = false;
        inputTiming = false;
        rigid = GetComponent<Rigidbody>();
        points.Add(this.gameObject.transform.position);
        currentLocation = 0;
    }

    public void Bind(List<Vector3> pts, List<string> sds, List<GameObject> plist)
    {
        points = pts;
        sides = sds;
        panelList = plist;
    }
    #endregion

    public void SetBeforeRun()
    {
        transform.position = new Vector3(startPos.position.x, 0.5f, startPos.position.z);
        nowPos = startPos.position;
        nextPos = points[0];
        currentLocation = 0;
        isJumping = false;
        points.Add(GameObject.FindWithTag("Finish").transform.position);
        panelList.Add(GameObject.FindWithTag("Finish"));
    }

    // !! TEST FOR USER INPUT DURING TIME
    public void testingInputTiming(){
        if(inputTiming){
            //Debug.Log("TRUE");
            testImage.SetActive(true);
        }
        if(!inputTiming){
            //Debug.Log("FALSE");
            testImage.SetActive(false);
        }
    }


    void Update()
    {
        // !! TEST FUNCTION !!
        testingInputTiming();
        Move();
        autoMoving();
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F) && inputTiming && sides[currentLocation].Equals("Left"))
        {
            // Left side panel pos.x = -1
            inputTiming = false;
            manager.CalculateScore(transform.position.y);
        }
        

        else if (Input.GetKeyDown(KeyCode.Space) && inputTiming && sides[currentLocation].Equals("Center"))
        {
            // Center panel pos.x = 0
            inputTiming = false;
            manager.CalculateScore(transform.position.y);
        }
        

        else if (Input.GetKeyDown(KeyCode.J) && inputTiming && sides[currentLocation].Equals("Right"))
        {
            // Right side panel pos.x = 1
            inputTiming = false;
            manager.CalculateScore(transform.position.y);
        }

        // else if((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.J)) && !inputTiming)
        // {
        //     // if player tried while jump
        //     Debug.Log("OOPS");
        //     rigid.velocity = new Vector3(0, -10, 0);
        // }

    }

    #region Move control section
    private float deltaTime = 1;
    private float forwardSpeed =  3.23f;// BPM30 1.8f; // 5.13512f; // BPM60 = 3.877308f
    private float halfPanelSize = 0.12f;
    private Vector3 nowPos;
    private Vector3 nextPos;

    private void autoMoving()
    {
        if(!isJumping)
            Jump();
    }
    void Jump()
    {
        deltaTime = (nextPos.z - nowPos.z - halfPanelSize) / forwardSpeed;
        Vector3 vector = rigid.velocity;
        vector.x = (nextPos.x - transform.position.x) / deltaTime;
        vector.y = -Physics.gravity.y * deltaTime / 2;
        vector.z = forwardSpeed;
        rigid.velocity = vector;

        isJumping = true;
        // Debug.Log((transform.position - nowPos).z);
        // Debug.Log(transform.position.y);
        // manager.CalculateScore(transform.position.y);
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
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Way Point" && currentLocation == 0)
        {
            SoundManager.Instance.PlaySongSound();
            // 싱크 디버깅용. 싱크 해결 되면 아래 코드 제거할 예정
            //GameObject.Find("SoundManager").transform.Find("Sync").gameObject.SetActive(true);
        }
        if (other.tag == "Way Point")
        {
            isJumping = false;
            inputTiming = true;
            panelList[currentLocation + 1].GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
            // next vs current
            // Debug.Log(sides[currentLocation]);
            nowPos = points[currentLocation];
            nextPos = points[currentLocation + 1];
            currentLocation++;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Way Point")
            inputTiming = false;
            
    }
}