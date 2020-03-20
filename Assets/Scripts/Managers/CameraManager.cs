using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private CinemachineFreeLook freeLook;  //Reference to free look camera

    private void Awake()
    {
        //Get a reference to the free look camera component
        freeLook = GetComponent<CinemachineFreeLook>();
    }

    public void SetCameraTarget(Transform targetTransform)
    {
        Debug.Log("Setting Character to cam");
        //Set the transform that the camera will floow and look at
        freeLook.Follow = targetTransform;
        freeLook.LookAt = targetTransform;
    }
}
