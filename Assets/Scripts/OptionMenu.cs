using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private AudioSource musicSound;
    private GameObject musicSoundObject;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private FloatVariables globalFloatVariables;

    void Start()
    {
        musicSoundObject = GameObject.FindGameObjectWithTag("MusicSound");

        if (musicSoundObject != null)
        {
            musicSound = musicSoundObject.GetComponent<AudioSource>();

            if (globalFloatVariables.musicVolume > 0)
            {
                volumeSlider.value = globalFloatVariables.musicVolume;
            }
            else
            {
                volumeSlider.value = musicSound.volume;
            }
        }

        globalFloatVariables.musicVolume = volumeSlider.value;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape") && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    public void ButtonOfGoBack()
    {
        gameObject.SetActive(false);
    }

    public void MusicVolume(float mixer)
    {
        if (musicSound == null)
        {
            return;
        }

        musicSound.volume = mixer;
        globalFloatVariables.musicVolume = musicSound.volume;
    }
}
