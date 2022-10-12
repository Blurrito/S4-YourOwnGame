using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSensitivityController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CameraObject;
    [SerializeField] float MouseHorizontalGain = 0.15f;
    [SerializeField] float MouseVerticalGain = 0.15f;
    [SerializeField] float JoystickHorizontalGain = 2f;
    [SerializeField] float JoystickVerticalGain = 2f;

    private int CameraStatus = -1;

    // Update is called once per frame
    void Update()
    {
        if (CameraObject == null)
        {
            CinemachinePOV Aim = CameraObject.GetCinemachineComponent<CinemachinePOV>();
            string[] JoystickNames = Input.GetJoystickNames();
            if (JoystickNames != null && Aim != null)
            {
                if (JoystickNames.Length > 0 && CameraStatus != 0)
                {
                    Aim.m_HorizontalAxis.m_MaxSpeed = JoystickHorizontalGain;
                    Aim.m_VerticalAxis.m_MaxSpeed = JoystickVerticalGain;
                    CameraStatus = 0;
                }
                else if (CameraStatus != 1)
                {
                    Aim.m_HorizontalAxis.m_MaxSpeed = MouseHorizontalGain;
                    Aim.m_VerticalAxis.m_MaxSpeed = MouseVerticalGain;
                    CameraStatus = 1;
                }
            }
        }
    }
}
