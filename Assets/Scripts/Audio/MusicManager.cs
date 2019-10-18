using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip gameMusic;
    public AudioClip menuMusic;

    private void Start()
    {
        AudioManager.audioManager.PlayMusic(menuMusic, 2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.audioManager.PlayMusic(gameMusic, 3);
        }
    }
}
