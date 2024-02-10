using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] AudioSource breathSound;
    public static event Action EndGameEvent;


    [SerializeField] float xBoundary = 10000;
    [SerializeField] float zBoundary = 10000;


    private void OnEnable()
    {
        MazeGenerator.onMazeGenerated += SetBoundary;
    }

    private void OnDisable()
    {
        MazeGenerator.onMazeGenerated -= SetBoundary;
    }

    public void SetBoundary(float xBoundary, float zBoundary)
    {
        this.xBoundary = xBoundary;
        this.zBoundary = zBoundary;
    }

    public void HandleMovement(Vector3 position)
    {
        if (!breathSound.isPlaying)
        {
            breathSound.Play();
        }
        if(xBoundary < position.x || zBoundary < position.z)
        {
            EndGameEvent?.Invoke();
        }

    }
}
