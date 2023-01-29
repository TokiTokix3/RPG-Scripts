using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSongActivator : MonoBehaviour
{
    public AudioManager audio;
    public AudioClip worldSong;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindObjectOfType<AudioManager>();
        audio.PlayMusic(worldSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
