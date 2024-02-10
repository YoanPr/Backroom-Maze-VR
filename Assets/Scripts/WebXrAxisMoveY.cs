using WebXR;
using Zinnia.Action;
using UnityEngine;

public class WebXrAxisMoveY : FloatAction
{

    [SerializeField] WebXRController controller;

    private float yAxis;

    // Update is called once per frame
    void Update()
    {
        yAxis = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).y;
        Receive(yAxis);
    }
}
