using WebXR;
using System;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    public static event Action StartGameEvent;
    

    [SerializeField] WebXRController controller;

    

    // Update is called once per frame
    void Update()
    {
            if (controller != null && controller.GetButtonDown(WebXRController.ButtonTypes.ButtonA))
            {
                StartGameEvent?.Invoke();
            }
    }
}
