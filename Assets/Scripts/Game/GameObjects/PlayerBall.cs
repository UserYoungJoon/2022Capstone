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
    private bool isPressed;
    private int sideNumber;
    private int count;
    #region Initializing section
    public void Init()
    {
        count = 4;
        isJumping = false;
        inputTiming = false;
        isPressed = false;
        rigid = GetComponent<Rigidbody>();
        points.Add(this.gameObject.transform.position);
        currentLocation = 0;
        sideNumber = 0;
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
        count = 4;
        nowPos = startPos.position;
        transform.position = nowPos;
        nextPos = points[0];
        currentLocation = 0;
        sideNumber = 0;
        isJumping = false;
        points.Add(GameObject.FindWithTag(TagType.FINISH).transform.position);
        panelList.Add(GameObject.FindWithTag(TagType.FINISH));
        sides.Add(TagType.FINISH);
        forwardSpeed = CSVConverter.mapDistance/(CSVConverter.lastTime/120)*(60/60)+2.22f;    //(60/bpm), 보정치2
         // Debug.Log("forwardSpeed:"+forwardSpeed +"lTime"+ CSVConverter.lastTime);
        gameObject.SetActive(true);
        rigid.velocity = new Vector3 (0, 0, 0);
        StartCoroutine(Countdown());
    }



    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && inputTiming && sides[sideNumber].Equals("Left"))
        {
            // Left side panel pos.x = -1
            inputTiming = false;
            isPressed = true;
            Debug.Log("왼쪽누름");
            manager.CalculateScore(transform.position.y);
        }
        

        else if (Input.GetKeyDown(KeyCode.Space) && inputTiming && sides[sideNumber].Equals("Center"))
        {
            // Center panel pos.x = 0
            inputTiming = false;
            isPressed = true;
            Debug.Log("가운데누름");
            manager.CalculateScore(transform.position.y);
        }
        

        else if (Input.GetKeyDown(KeyCode.J) && inputTiming && sides[sideNumber].Equals("Right"))
        {
            // Right side panel pos.x = 1
            inputTiming = false;
            isPressed = true;
            Debug.Log("오른쪽누름");
            manager.CalculateScore(transform.position.y);
        }
    }
    IEnumerator Countdown()
    {
        Debug.Log("Coroutine Working");
        Debug.Log("Called");
        while(true)
        {
            yield return new WaitForSecondsRealtime(1);
            if(count == 0)
            {
                Debug.Log("멈춰야됨");
                break;
            }
            count = count - 1;
            Debug.Log(count + "초 남음");
        }
    }

    private void Update()
    {
        if(count == 0)
        {
            StopCoroutine(Countdown());
            Move();
            autoMoving();
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
            UserInput();
        }
    }

    #region Move control section
    private float deltaTime = 1;
    
    //private float forwardSpeed =  3.877308f;// BPM30 1.8f; // 5.13512f; // BPM60 = 3.877308f distance/time
    private float forwardSpeed = -1;
    private float halfPanelSize = 0.025f;
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
        vector.y = -Physics.gravity.y * deltaTime / 2 + 0.1f;   //보정치0.1
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
        }
        if (other.tag == TagType.WAY_POINT)
        {
            isJumping = false;
            if(panelList[currentLocation + 1].gameObject.tag == "Finish")
            {
                panelList[currentLocation + 1].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
            else {
                panelList[currentLocation + 1].transform.Find("ring").GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
            }
            
            nowPos = points[currentLocation];
            nextPos = points[currentLocation + 1];
            //Debug.LogFormat("{0}, {1}", nextPos.x, nextPos.z);
            currentLocation++;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Item" && isPressed == false){
            inputTiming = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Item")
        {
            if(inputTiming & !isPressed)
            {
                Debug.Log("외않눌음");
            }
            inputTiming = false;
            isPressed = false;
            sideNumber += 1;
        }
    }
}