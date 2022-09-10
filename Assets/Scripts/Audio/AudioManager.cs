using UnityEngine;

[System.Serializable]
public class Sound
{
    public TypeOfSound Name;
    public AudioClip Clip;

    [Range (0f, 1f)]
    public float Volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;

    [Range(0f, 0.5f)]
    public float RandomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float RandomPitch = 0.1f;

    public bool Loop = false;

    private AudioSource _source;

    public void SetSource(AudioSource audioSource)
    {
        _source = audioSource;
        _source.clip = Clip;
        _source.loop = Loop;
    }

    public void Play()
    {
        _source.volume = Volume * (1 + Random.Range(-RandomVolume / 2f, RandomVolume / 2f));
        _source.pitch = Pitch * (1 + Random.Range(-RandomPitch / 2f, RandomPitch / 2f));
        _source.Play();
    }

    public void Stop()
    {
        _source.Stop();
    }
}
public class AudioManager : MonoBehaviour, IAudioManager
{
    public static IAudioManager Instance;

    [SerializeField] private Sound[] SoundsList;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        for (int i = 0; i < SoundsList.Length; i++)
        {
            var go = new GameObject("Sound_" + i + "_" + SoundsList[i].Name);
            go.transform.SetParent(this.transform);
            SoundsList[i].SetSource(go.AddComponent<AudioSource>());
        }
    }
   
    public void PlaySound(TypeOfSound soundName)
    {
        for (int i = 0; i < SoundsList.Length; i++)
        {
            if (SoundsList[i].Name == soundName)
            {
                SoundsList[i].Play();
                return;
            }
        }

        //No sound with _name.
        Debug.LogWarning("AudioManager: Sound not found in list: " + soundName);
    }

    public void StopSound(TypeOfSound soundName)
    {
        for (int i = 0; i < SoundsList.Length; i++)
        {
            if (SoundsList[i].Name == soundName)
            {
                SoundsList[i].Stop();
                return;
            }
        }

        //No sound with _name.
        Debug.LogWarning("AudioManager: Sound not found in list: " + soundName);
    }

}

public enum TypeOfSound { FirstStar, SecondStar, ThirdStar, WholePuzzle}
