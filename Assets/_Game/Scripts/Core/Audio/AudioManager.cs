using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioClip[] musicClips;
    [SerializeField]
    private AudioClip[] soundClips;

    private AudioData audioData;

    public void Initialize()
    {
        audioData = DataManager.Instance.gameData.audio;
        ToggleAudioSource();
    }

    public void ToggleAudioSource()
    {
        musicSource.enabled = audioData.isMusicEnabled;
        soundSource.enabled = audioData.isSoundEnabled;
    }

    public void PlayMusic(MusicId id)
    {
        musicSource.clip = musicClips[(int)id];
        musicSource.Play();
    }

    public void PlaySound(SoundId id)
    {
        if (soundSource.loop)
        {
            soundSource.loop = false;
            soundSource.Stop();
        }

        soundSource.PlayOneShot(soundClips[(int)id]);
    }

    public void PlaySound(SoundId id, float duration)
    {
        soundSource.loop = true;
        soundSource.clip = soundClips[(int)id];
        soundSource.Play();

        ScheduleUtils.DelayTask(duration, () =>
        {
            if (soundSource.loop)
            {
                soundSource.loop = false;
                soundSource.Stop();
            }
        });
    }
}

public enum MusicId
{
    [HideInInspector] None = -1,
    Home = 0,
    Game = 1,
}

public enum SoundId
{
    [HideInInspector] None = -1,
    Click = 0,
}
