using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip soundClip;
        public SoundType type;
    }
 

    public static SoundManager Instance = null;

    [SerializeField] private List<Sound> sounds;
    [SerializeField] private AudioSource soundSource;

    [SerializeField] private int poolSize = 5;
    private Queue<AudioSource> audioSourcePool;

    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        InitializeAudioSourcePool();
    }

    private void InitializeAudioSourcePool()
    {
        audioSourcePool = new Queue<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            audioSourcePool.Enqueue(newAudioSource);
        }
    }

    public void Play(SoundType type)
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            AudioClip clipToPlay = sounds.Find(x => x.type == type).soundClip;
            AudioSource availableSource = GetAvailableAudioSource();
            availableSource.clip = clipToPlay;
            availableSource.Play();
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        AudioSource audioSource = audioSourcePool.Dequeue();

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSourcePool.Enqueue(audioSource);
        return audioSource;
    }

}