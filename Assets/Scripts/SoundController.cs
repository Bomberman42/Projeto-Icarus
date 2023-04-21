using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource musicSound;
    private GameObject musicSoundObject;
    [SerializeField]
    private FloatVariables globalFloatVariables;

    // Start is called before the first frame update
    void Start()
    {
        musicSoundObject = GameObject.FindGameObjectWithTag("MusicSound");

        if (musicSoundObject != null)
        {
            musicSound = musicSoundObject.GetComponent<AudioSource>();

            if (globalFloatVariables.musicVolume > 0)
            {
                musicSound.volume = globalFloatVariables.musicVolume;
            }
        }
    }
}
