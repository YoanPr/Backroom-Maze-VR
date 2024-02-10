using WebXR;
using Zinnia.Action;
using UnityEngine;

public class WebXrAxisMoveX : FloatAction
{

    [SerializeField] WebXRController controller;

    private float xAxis;

    // Update is called once per frame
    void Update()
    {
        xAxis = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).x;
        Receive(xAxis);
    }
}
