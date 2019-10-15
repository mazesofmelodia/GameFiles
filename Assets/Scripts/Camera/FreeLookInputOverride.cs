using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]     //This class needs a free look camera to work
public class FreeLookInputOverride : MonoBehaviour
{
    private CinemachineFreeLook freeLookCam;        //Free look camera
    private float newXInput = 0f;                   //New X input for the camera
    private float newYInput = 0f;                   //New Y input for the camera

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the Free Look Camera
        freeLookCam = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    /*void Update()
    {
        //Update the free look x and y axis to the new x and y inputs
        freeLookCam.m_XAxis.Value = newXInput;
        freeLookCam.m_YAxis.Value = newYInput;
    }*/

    public void OnRotate(InputAction.CallbackContext context){
        //Change new x input to the rotate input value
        newXInput = context.ReadValue<float>();
    }

    public void OnZoom(InputAction.CallbackContext context){
        //Change new y input to the rotate input value
        newYInput = context.ReadValue<float>();
    }
}
