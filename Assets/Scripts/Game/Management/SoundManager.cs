using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�� ���ӿ����� �Ÿ��� ����� ������ ũ�⸦ ������ �ʿ䰡 ���⿡ �ϳ��� AudioSource�� AudioClip���� �������� �����ų ���̴�.
//��������� ������ AudioSource�� ȿ������ ������ AudioSource�� SoundManager�� �ڽ� ������Ʈ�� ����

//https://velog.io/@uchang903/Unity-SoundManager-%EC%8A%A4%ED%81%AC%EB%A6%BD%ED%8A%B8
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    } // Sound�� �������ִ� ��ũ��Ʈ�� �ϳ��� �����ؾ��ϰ� instance������Ƽ�� ���� ��𿡼��� �ҷ��������� �̱��� ���

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;
    private AudioSource songPlayer;

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    [SerializeField]
    private AudioClip mainBgmAudioClip; //����ȭ�鿡�� ����� BGM


    [SerializeField]
    private AudioClip[] sfxAudioClips; //ȿ������ ����

    [SerializeField]
    private AudioClip[] songAudioClips; // �� ����Ʈ ����

    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>(); //ȿ���� ��ųʸ�
    // AudioClip�� Key,Value ���·� �����ϱ� ���� ��ųʸ� ���
    Dictionary<string, AudioClip> audioSongDic = new Dictionary<string, AudioClip>(); // ��Ʈ�� ���� ��ųʸ�
    public static string selectedSong; // UI���� �� ���� �� �Է� �� ����

    private void Awake()
    {
        selectedSong = "60AirplaneBGM"; // �ʱ� ���� Airplane
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); //���� ������ ����� ��.

        bgmPlayer = GameObject.Find("MainMenuPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
        songPlayer = GameObject.Find("SongPlayer").GetComponent<AudioSource>();

        // SoundManager AudioClip Element �߰� - SFX
        foreach (AudioClip audioclip in sfxAudioClips)
        {
            audioClipsDic.Add(audioclip.name, audioclip);
        }
        // SoundManager AudioClip Element �߰� - Song
        foreach (AudioClip audioclip in songAudioClips)
        {
            audioSongDic.Add(audioclip.name, audioclip);
        }
    }


    // ȿ�� ���� ��� : �̸��� �ʼ� �Ű�����, ������ ������ �Ű������� ����
    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false) // Element�� ���� ��
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }

    //BGM ���� ��� : ������ ������ �Ű������� ����
    public void PlayBGMSound(float volume = 1f)
    {
        bgmPlayer.loop = true; //BGM �����̹Ƿ� ��������
        bgmPlayer.volume = volume * masterVolumeBGM;

        bgmPlayer.clip = mainBgmAudioClip;
        bgmPlayer.Play();
        
        //���� ���� �´� BGM ���
    }

    public void PlaySongSound(float volume = 1f)
    {
        if(audioSongDic.ContainsKey(selectedSong) == false) // Element�� ���� ��
        {
            Debug.Log((selectedSong + " is not Contained audioSongDic"));
            return;
        }
        songPlayer.clip = audioSongDic[selectedSong];
        songPlayer.volume = volume + 10;    
        songPlayer.Play();
    }

    //���� BGM�� �ð�
    public void getTime()
    {
        Debug.Log("current Time: " + bgmPlayer.time);
    }

    public void StopBGMSound()
    {
        bgmPlayer.Stop();
    }

    public void StopSongSound()
    {
        songPlayer.Stop();
    }
    

    // ����ڰ� ESC Ű�� �Է��Ͽ� ������ ���߾��� ��
    public void PauseSong()
    {
        songPlayer.Pause();
    }
}

//          Algorithm when player pause the game
// if Player GetKeyDown KeyCode.ESC and !gameIsStopped
//     then songPlayer.Pause
//     set gameIsStopped = true
// if Player GetKeyDown KeyCode.ESC and gameIsStopped
//     then songPlayer.Play
//     set gameIsStopped = false

// UI ��ư Ŭ�� �� SFX?
// �гο� SFX ������