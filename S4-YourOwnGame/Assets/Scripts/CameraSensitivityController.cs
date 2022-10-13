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

    private void Start()
    {
        if (CameraObject != null)
        {
            CinemachinePOV Aim = CameraObject.GetCinemachineComponent<CinemachinePOV>();
            string[] JoystickNames = Input.GetJoystickNames();

            if (JoystickNames != null && Aim != null)
            {
                if (JoystickNames.Length > 0)
                {
                    Aim.m_HorizontalAxis.m_MaxSpeed = JoystickHorizontalGain;
                    Aim.m_VerticalAxis.m_MaxSpeed = JoystickVerticalGain;
                }
                else
                {
                    Aim.m_HorizontalAxis.m_MaxSpeed = MouseHorizontalGain;
                    Aim.m_VerticalAxis.m_MaxSpeed = MouseVerticalGain;
                }
            }
        }
    }
}
