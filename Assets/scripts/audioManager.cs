using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class audioManager : MonoBehaviour
{
    static audioManager _instance;
    public Sprite[] sprtSound;
    private bool isSoundEnable = true;

    public Sound[] sounds;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        Play("Background");
    }

    public void Play (string name)
    {
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
        }
    }
    
    public void VolumeChange(string name, float percentage)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.volume = percentage;
        }
    }

    public void VolumeEnable()
    {
        Image go = GameObject.Find("/Canvas/PauseMenu/Audio").GetComponent<Image>();
        if (isSoundEnable)
        {
            go.sprite = sprtSound[1];
            AudioListener.volume = 0f;
            isSoundEnable = !isSoundEnable;
        }
        else
        {
            go.sprite = sprtSound[0];
            AudioListener.volume = 1.0f;
            Play("Click");
            isSoundEnable = !isSoundEnable;
        }
    }
}
