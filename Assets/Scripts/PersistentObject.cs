using UnityEngine;
using static UnityEngine.Audio.ProcessorInstance;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource audioSource;
    public AudioClip[] musicTracks;

    private float musicVolume = 0.03f;

    public static bool PlayerAudioPlaying = true;

    private bool MusicPlaying = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Load all AudioClips from Assets/Resources/Music
        object[] loadedClips = Resources.LoadAll("Music", typeof(AudioClip));

        musicTracks = new AudioClip[loadedClips.Length];
        for (int i = 0; i < loadedClips.Length; i++)
        {
            musicTracks[i] = (AudioClip)loadedClips[i];
        }

        PlayNextRandomSong();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MusicPlaying = !MusicPlaying;
        }

        if(MusicPlaying)
        {
            audioSource.volume = musicVolume;
        }
        else
        {
            audioSource.volume = 0f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            musicVolume += 0.05f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && musicVolume > 0.03f)
        {
            musicVolume -= 0.05f * Time.deltaTime;
        }
        if(musicVolume < 0.03f)
        {
            musicVolume = 0.03f;
        }
        if (!audioSource.isPlaying && musicTracks.Length > 0)
        {
            PlayNextRandomSong();
        }
    }

    void PlayNextRandomSong()
    {
        int randomIndex = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[randomIndex];
        audioSource.Play();
    }
}
