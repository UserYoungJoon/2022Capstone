using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public List<Vector3> points = null;
    public List<string> sides = null;
    public List<GameObject> panelList = null;
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
        nowPos = startPos.position;
        transform.position = nowPos;
        nextPos = points[0];
        currentLocation = 0;
        isJumping = false;
        points.Add(GameObject.FindWithTag(TagType.FINISH).transform.position);
        gameObject.SetActive(true);
    }


    void Update()
    {
        // !! TEST FUNCTION !!
        // testingInputTiming();
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

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == TagType.WAY_POINT && currentLocation == 0)
        {
            SoundManager.Instance.PlaySongSound();
            // 싱크 디버깅용. 싱크 해결 되면 아래 코드 제거할 예정
            //GameObject.Find("SoundManager").transform.Find("Sync").gameObject.SetActive(true);
        }
        if (other.tag == TagType.WAY_POINT)
        {
            isJumping = false;
            panelList[currentLocation + 1].GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
            
            nowPos = points[currentLocation];
            nextPos = points[currentLocation + 1];
            //Debug.LogFormat("{0}, {1}", nextPos.x, nextPos.z);
            currentLocation++;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Item"){
            inputTiming = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Way Point")
            inputTiming = false;
            
    }
}