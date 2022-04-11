using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Written by https://gist.github.com/bzgeb/c298c6189c73b2cf777c
public class Sync : MonoBehaviour
{
    public double bpm = 105.0F; // 정적 처리, 나중에 곡 정보를 불러와서 이 곳에 bpm 들어갈 예정

    double nextTick = 0.0F; // The next tick in dspTime
    double sampleRate = 0.0F; 
    bool ticked = false;

    void Start() {
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;

        nextTick = startTick + (60.0 / bpm);
    }

    void LateUpdate() {
        if ( !ticked && nextTick >= AudioSettings.dspTime ) {
            ticked = true;
            BroadcastMessage( "OnTick" );
        }
    }

    // Just an example OnTick here
    void OnTick() {
        Debug.Log( "Tick" );
        GetComponent<AudioSource>().Play();
    }

    void FixedUpdate() {
        double timePerTick = 60.0f / bpm;
        double dspTime = AudioSettings.dspTime;

        while ( dspTime >= nextTick ) {
            ticked = false;
            nextTick += timePerTick;
        }

    }
}
