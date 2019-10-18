using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    float masterVolume = 1;
    float sfxVolume = 1;
    float musicVolume = 1;

    public AudioSource[] musicSources;
    public int activeMusicSourceIndex;

    public static AudioManager audioManager;

    private void Awake()
    {
        audioManager = this;
        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("MusicSource" + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1f)
    {
        activeMusicSourceIndex = 2 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();
        StartCoroutine(CrossFade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVolume * masterVolume);
    }

    IEnumerator CrossFade(float duration)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolume * masterVolume, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolume * masterVolume, 0, percent);
            yield return null;
        }
    }
}
