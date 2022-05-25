using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [System.Serializable]
    public enum Sound
    {
        EnemyFarSpawn
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
        [Range(0, 1)]
        public float volume;
    }

    [Header("Sounds")]
    [SerializeField] private SoundsSO soundsScriptableObject = null;

    public static AudioSystem Instance { get; private set; }

    private static AudioSource oneShotAudioSource;
    private static GameObject oneShotGameObject;

    private void Awake()
    {
        //Make sure there is only 1 singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public SoundAudioClip SACFromSound(Sound sound)
    {
        SoundAudioClip[] sounds = soundsScriptableObject.sounds;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].sound == sound)
            {
                return sounds[i];
            }
        }

        Debug.LogError($"Could not find AudioClip for Sound {sound}");
        return null;
    }

    // When used in Inspector enums don't work so we have to use a class
    //public void PlaySound(SoundHolder sound)
    //{
    //    if (oneShotGameObject == null)
    //    {
    //        oneShotGameObject = new GameObject("One Shot Sound");
    //        oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
    //    }
    //    SoundAudioClip sacObject = SACFromSound(sound.Sound);
    //    oneShotAudioSource.volume = sacObject.volume;
    //    oneShotAudioSource.PlayOneShot(sacObject.audioClip);
    //}

    public void PlaySound(Sound sound)
    {
        //if (oneShotGameObject == null)
        //{
        //    oneShotGameObject = new GameObject("One Shot Sound");
        //    oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        //}
        //SoundAudioClip sacObject = SACFromSound(sound);
        //if (sacObject != null)
        //{
        //    oneShotAudioSource.volume = sacObject.volume;
        //    oneShotAudioSource.PlayOneShot(sacObject.audioClip);
        //}

        GameObject audioObject = new GameObject("Sound");
        AudioSource audio = audioObject.AddComponent<AudioSource>();
        SoundAudioClip sacObject = SACFromSound(sound);
        audio.volume = sacObject.volume;
        audio.PlayOneShot(sacObject.audioClip);
        Object.Destroy(audioObject, sacObject.audioClip.length);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
