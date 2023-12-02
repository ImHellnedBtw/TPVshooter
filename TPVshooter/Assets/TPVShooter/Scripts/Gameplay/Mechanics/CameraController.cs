using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour 
{
    public CinemachineVirtualCamera VirtualCameraOne;
    public CinemachineVirtualCamera VirtualCameraTwo;
    public RawImage sniperCrosshair;
    public RawImage defaultCrosshair;
    private bool inScope;

    public void ChangeCamera(bool isSniper)
    {
        VirtualCameraTwo.m_Lens.FieldOfView = 90;
        if (inScope == false)
        {
            VirtualCameraTwo.gameObject.SetActive(true);
            VirtualCameraOne.gameObject.SetActive(false);
            inScope = true;
            if(isSniper == true)
            {
                VirtualCameraTwo.m_Lens.FieldOfView = 20;
                defaultCrosshair.gameObject.SetActive(false);
                sniperCrosshair.gameObject.SetActive(true);
            }
        }else
        {
            VirtualCameraOne.gameObject.SetActive(true);
            VirtualCameraTwo.gameObject.SetActive(false);
            inScope=false;
            if (isSniper == true)
            {
                sniperCrosshair.gameObject.SetActive(false);
                defaultCrosshair.gameObject.SetActive(true);
            }
        }
    }

}
