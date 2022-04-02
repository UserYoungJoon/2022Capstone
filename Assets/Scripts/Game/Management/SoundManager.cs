using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }


            return instance;
        }
    }
    public enum eSound
    {
        BGM,
        sfx,
        MaxCount,
    }

    public AudioSource[] _audioSources = new AudioSource[(int)eSound.MaxCount];
    public Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();


    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound"};
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(eSound)); // "BGM", "sfx"
            for(int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i]};
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)eSound.BGM].loop = true;
        }
    }

    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 딕셔너리 비우기
        _audioClips.Clear();

    }

    public void Play(AudioClip audioClip, eSound type = eSound.sfx, float pitch = 1.0f)
    {
        if(audioClip == null)
            return;

        // BGM 배경음악 재생
        if(type == eSound.BGM)
        {
            AudioSource audioSource = _audioSources[(int)eSound.BGM];
            if(audioSource.isPlaying)
                audioSource.Stop();
            
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)eSound.sfx];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, eSound type = eSound.sfx, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }



    public AudioClip GetOrAddAudioClip(string path, eSound type = eSound.sfx)
    {
        if(path.Contains("Resources/") == false)
            path = $"Resources/{path}"; // Resources 폴더 안에 저장될 수 있도록

        AudioClip audioClip = null;

        if(type == eSound.BGM)
        {
            audioClip = Resources.Load<AudioClip>(path);
            
        }

        else // sfx 효과음 클립 붙이기
        {
            if(_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if(audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }


}