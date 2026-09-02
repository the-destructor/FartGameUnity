using UnityEngine;
using static UnityEngine.Audio.ProcessorInstance;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;

    public static bool CompletedLoading = false;
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

    private AudioSource audioSource;
    private string[] musicFiles;
    public AudioClip[] musicTracks;

    private float musicVolume = 0.03f;

    public static bool PlayerAudioPlaying = true;
    public static int FileCount;
    public static int FilesLoaded = 0;

    private bool MusicPlaying = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        string musicPath = Path.Combine(Application.streamingAssetsPath, "Music");

        if (Directory.Exists(musicPath))
        {
            musicFiles = Directory.GetFiles(musicPath, "*.mp3");
            StartCoroutine(LoadMusic());
        }
        else
        {
            Debug.LogError("Music folder not found: " + musicPath);
        }


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
        if (!audioSource.isPlaying && musicTracks.Length > 0 && CompletedLoading)
        {
            PlayNextRandomSong();
        }
    }

    IEnumerator LoadMusic()
    {
        musicTracks = new AudioClip[musicFiles.Length];
        FileCount = musicFiles.Length;
        for (int i = 0; i < musicFiles.Length; i++)
        {
            string filePath = musicFiles[i];

            // Convert the file path into a URI that UnityWebRequest can use.
            string url = "file://" + filePath;

            using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(
                url,
                AudioType.MPEG))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Failed to load music: " + filePath +
                                   "\n" + request.error);
                    continue;
                }

                musicTracks[i] = DownloadHandlerAudioClip.GetContent(request);
            }
            FilesLoaded += 1;
        }
        CompletedLoading = true;
        PlayNextRandomSong();
    }
    void PlayNextRandomSong()
    {
        int randomIndex = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[randomIndex];
        audioSource.Play();
    }
}
