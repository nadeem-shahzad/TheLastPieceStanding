using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public enum SoundType
    {
        Click, Background, UnderControl, StageComplete, Loose, Bullet, LooseControl
    }

    [SerializeField] private AudioClip click, background, underControl, stageComplete, loose, bullet, looseControl;
    [SerializeField] private AudioSource backgroundAS;
    [SerializeField] private AudioSource audioSource;




    public bool isMusicOn { get; set; }


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayBackGroundSound(DataManager.Instance.IsSoundOn);
    }


    public void PlayBackGroundSound(bool isOn)
    {
        if (isOn == true)
        {
            backgroundAS.Play();
        }
        else
        {
            backgroundAS.Stop();
        }
    }


    public void PlaySound(SoundType soundType)
    {
        if (DataManager.Instance.IsMusicOn)
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
            case SoundType.Bullet:
                return bullet;
            case SoundType.StageComplete:
                return stageComplete;
            case SoundType.UnderControl:
                return underControl;
            case SoundType.Background:
                return background;
            case SoundType.Loose:
                return loose;
            case SoundType.LooseControl:
                return looseControl;
            default:
                return null;
        }
    }



}