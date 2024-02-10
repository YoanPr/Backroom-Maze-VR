using System;
using UnityEngine;
using UnityEngine.UI;

// Update the music volume based on the menu slider
// Update the game settings and the menu music
public class ChangeVolume : MonoBehaviour
{

    [SerializeField] AudioSource menuMusic;
    [SerializeField] float changeSensiblity;

    private Slider slider;
    private float currentVolume;
    private GameSettings gameSettings;

    void Start()
    {
        slider = GetComponent<Slider>();
        gameSettings = GameSettings.Instance;
        float initialVolume = gameSettings.SoundVolume;
        slider.value = initialVolume;
        currentVolume = initialVolume;
    }

    public void OnChangeVolume(float value)
    {
        float newValue = Mathf.Clamp01(currentVolume + changeSensiblity * value);
        menuMusic.volume = newValue;
        gameSettings.SoundVolume = newValue;
        slider.value = newValue;
        currentVolume = newValue;
    }
}
