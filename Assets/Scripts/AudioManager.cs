using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton pattern to ensure only one AudioManager instance exists
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    // Add an AudioSource component to play music
    private AudioSource musicPlayer;

    // Add an AudioClip variable to hold your music
    public AudioClip backgroundMusic;

    // Add a public property for controlling the volume
    public float musicVolume = 1.0f; // Default volume is 100%

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = Mathf.Clamp01(value);
            if (musicPlayer != null)
            {
                musicPlayer.volume = musicVolume;
            }
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        musicPlayer = gameObject.AddComponent<AudioSource>();
        musicPlayer.loop = true;
        musicPlayer.clip = backgroundMusic;

        PlayMusic();
    }

    public void PlayMusic()
    {
        if (!musicPlayer.isPlaying)
        {
            musicPlayer.volume = musicVolume;
            musicPlayer.Play();
        }
    }
}
