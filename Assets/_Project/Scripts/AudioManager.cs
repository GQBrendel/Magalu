using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(this.gameObject);
    }

    public void Play(EventReference sound)
    {
        EventInstance eventInstance;

        eventInstance = RuntimeManager.CreateInstance(sound);
        eventInstance.start();
        eventInstance.release();
    }
}
