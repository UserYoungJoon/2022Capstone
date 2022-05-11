using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    public CameraMoving cameraMoving;

    // Game Mode
    public PlayerBall playerBall;
    public Transform beatMap;
    public GameObject panelPrefab;
    public UIManager uIManager;

    private CSVConverter CSVConverter;
    private Notes notes;

    private int userScore = 0; // ���� ���� �� ������ 0���� �ʱ�ȭ

    void Awake()
    {
        //Game Manager Initial
        CSVConverter = new CSVConverter();
        notes = new Notes();

        //Initial
        InitWorldGenerator();
        InitGameObj();
        notes.Init();
        CSVConverter.Init();
        cameraMoving.Init();

        //Bind
        playerBall.Bind(CSVConverter.wayPointsList, CSVConverter.panelsSideList, CSVConverter.panelList);
        cameraMoving.Bind(playerBall.transform);
        CSVConverter.Bind(panelPrefab, beatMap, notes);
        uIManager.Bind(this);

        //Clear
        playerBall.gameObject.SetActive(false);

        //���⼭ �������� ���� �뷡 ���� UI �����ִٰ� �����ϸ� �� �� �����ϵ��� �ϱ�
        SwitchGameState(eGameState.SELECT_SONG);
    }

    private void InitGameObj()
    {
        playerBall.Init();
        floor.Init();
    }

    public enum eGameState
    {
        SELECT_SONG,
        GAME,
        NONE
    }
    private eGameState gameState;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && gameState != eGameState.GAME)
        {
            gameState = eGameState.GAME;
            SwitchGameState(eGameState.GAME);
        }
    }

    public void SwitchGameState(eGameState to)
    {
        gameState = to;
        switch (gameState)
        {//Ȱ��ȭ�� ��Ȱ��ȭ 
            case eGameState.SELECT_SONG:
                {
                    //plz Destroy ingame world....
                    uIManager.SetSelectMode();
                    cameraMoving.SetSelectMode();
                }
                break;
            case eGameState.GAME:
                {
                    uIManager.SetGameMode();
                    cameraMoving.SetGameMode();

                    SetBeforeGenerate();
                    GenerateWorld();

                    userScore = 0;
                    playerBall.gameObject.SetActive(true);
                    playerBall.SetBeforeRun();
                }
                break;
            case eGameState.NONE:
                {
                    Debug.Log("ERROR : GAME MODE was [NONE]");
                }
                break;
            default:
                break;
        }
    }
}

public static class Timer
{
    public static void FreezeTime()
    {
        Time.timeScale = 0;
    }

    public static void MeltTime()
    {
        Time.timeScale = 1;
    }
}