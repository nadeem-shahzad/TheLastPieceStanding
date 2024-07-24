using System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public enum SoundType
    {
        Click, LevelUp, LevelLose, Move, Capture
    }

    [SerializeField] private AudioClip click, levelUp, levelLose, move, capture;
    [SerializeField] private AudioSource audioSource;




    
    
    public void PlaySound(SoundType soundType)
    {
        if (DataManager.Instance.IsSoundOn)
        {
            AudioClip audioClip = GetAudioClip(soundType);
            if (this.audioSource.isPlaying)
            {
                AudioSource audioSource = GetAudioSource();
                audioSource.clip = audioClip;
                audioSource.gameObject.AddComponent<DestroyAudioSource>();
                audioSource.Play();
            }
            else
            {
                this.audioSource.Stop();
                this.audioSource.clip = audioClip;
                this.audioSource.Play();
            }
        }
    }


    AudioSource GetAudioSource()
    {
        GameObject audioSourceGameObject = new GameObject();
        AudioSource audioSource = audioSourceGameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        return audioSource;
    }


    AudioClip GetAudioClip(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Click:
                return click;
            case SoundType.LevelUp:
                return levelUp;
            case SoundType.LevelLose:
                return levelLose;
            case SoundType.Move:
                return move;
            case SoundType.Capture:
                return capture;
            default:
                return null;
        }
    }



}