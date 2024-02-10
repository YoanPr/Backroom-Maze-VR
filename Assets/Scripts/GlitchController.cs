using UnityEngine;

// Control the glitching effet at the end of the maze https://www.youtube.com/watch?v=VgBv6OrYY7E
public class GlitchController : MonoBehaviour
{
    // Material that store the shader
    [SerializeField] Material glitchMat;
    // power of the noise
    [SerializeField] float noise;
    // refresh rate of the glitch
    [SerializeField] float glitchRate;
    // Size of the end game circle
    [SerializeField] float fadeSize;
    // Color of the scan line (1 = transparent, 0 = black)
    [SerializeField] float scanLine = 1;
    [SerializeField] bool IsSoundEnable = false;
    // Sound that the glitch effect needs to disable
    [SerializeField] AudioSource[] soundToDisabled;

    private AudioSource audioGlitch;

    private void Start()
    {
        audioGlitch = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Update the shader based on the value of the script 
        glitchMat.SetFloat("_Noise", noise);
        glitchMat.SetFloat("_GlitchRate", glitchRate);
        glitchMat.SetFloat("_ScanLine", scanLine);
        glitchMat.SetFloat("_FadeSize", fadeSize);
        // Start the glitch sound
        if (IsSoundEnable && !audioGlitch.isPlaying)
        {
            audioGlitch.Play();
            for(int i = 0; i < soundToDisabled.Length; i++)
            {
                soundToDisabled[i].volume = 0;
            }
        }
    }
}
