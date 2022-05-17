using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// https://velog.io/@uchang903/Unity-SoundManager-%EC%8A%A4%ED%81%AC%EB%A6%BD%ED%8A%B8
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
    }
    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;
    private AudioSource songPlayer;

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;
    public float volume = 1f;

    [SerializeField]
    private AudioClip mainBgmAudioClip;


    [SerializeField]
    private AudioClip[] sfxAudioClips; 

    [SerializeField]
    private AudioClip[] songAudioClips; 

    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>(); 

    Dictionary<string, AudioClip> audioSongDic = new Dictionary<string, AudioClip>();
    public string selectedSong; 

    private void Awake()
    {
        selectedSong = "60AirplaneBGM";
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); 

        bgmPlayer = GameObject.Find("MainMenuPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
        songPlayer = GameObject.Find("SongPlayer").GetComponent<AudioSource>();

        // SoundManager AudioClip Element  - SFX
        foreach (AudioClip audioclip in sfxAudioClips)
        {
            audioClipsDic.Add(audioclip.name, audioclip);
        }
        // SoundManager AudioClip Element  - Song
        foreach (AudioClip audioclip in songAudioClips)
        {
            audioSongDic.Add(audioclip.name, audioclip);
        }
    }

    public void SetVolumne(float volume)
    {
        this.volume = volume;
    }
    public void PlaySFXSound(string name)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }

    public void PlayBGMSound()
    {
        bgmPlayer.loop = true; 
        bgmPlayer.volume = volume * masterVolumeBGM;

        bgmPlayer.clip = mainBgmAudioClip;
        bgmPlayer.Play();
        
    }

    public void PlaySongSound()
    {
        if(audioSongDic.ContainsKey(selectedSong) == false) 
        {
            Debug.Log((selectedSong + " is not Contained audioSongDic"));
            return;
        }
        songPlayer.clip = audioSongDic[selectedSong];
        songPlayer.volume = volume;
        songPlayer.Play();
    }

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
