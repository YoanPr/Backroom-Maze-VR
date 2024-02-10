using UnityEngine;

// Setup an AudioSource by setting the volume to the volume set in the menu
public class SetupSound : MonoBehaviour
{
    void Start()
    {
        float volume = GameSettings.Instance.SoundVolume;
        AudioSource audioS = gameObject.GetComponent<AudioSource>();
        audioS.volume = audioS.volume * volume;
    }

}
