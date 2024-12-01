using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip; 

    public KeyCode key;

    public float volume;   

    public bool loop;

    public bool playOnAwake;

    public AudioMixerGroup mixerGroup;

    public bool isBGM;

}