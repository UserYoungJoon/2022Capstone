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
    //private float forwardSpeed = 1.8f;
    private float forwardSpeed = 3.877308f; // 5.13512f
    private float halfPanelSize = 0.12f;
    private Vector3 nowPos;
    private Vector3 nextPos;

    private void autoMoving()
    {
        Jump();
    }
    void Jump()
    {
        deltaTime = (nextPos.z - nowPos.z - halfPanelSize) / forwardSpeed;
        Vector3 vector = rigid.velocity;
        vector.x = (nextPos.x - transform.position.x) / deltaTime;
        vector.y = -Physics.gravity.y * deltaTime / 2;
        Debug.Log("여기가 포워드 스피드" + forwardSpeed);
        vector.z = forwardSpeed;
        rigid.velocity = vector;

        isJumping = true;
        Debug.Log((transform.position - nowPos).z);
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
            SoundManager.Instance.PlaySongSound();
            // 싱크 디버깅용. 싱크 해결 되면 아래 코드 제거할 예정
            //GameObject.Find("SoundManager").transform.Find("Sync").gameObject.SetActive(true);
        }
        if (other.tag == "Way Point")
        {
            nowPos = points[currentLocation];
            nextPos = points[currentLocation + 1];
            currentLocation++;
        }
    }
}

// 별도의 보고서 필요 없음, PPT 발표 자료만 준비해라, 수정된 사항이나 추진 계획 포맷은 과제방에 공유할꺼임
// 4월 28일 중간발표 논문 5월 10일까지
// 안된 팀 교수랑 매칭인가? 추후에 통보함
// 물품신청 마감