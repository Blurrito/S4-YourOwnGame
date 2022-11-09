using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEffect : MonoBehaviour
{
    [SerializeField] AudioSource Source;
    [SerializeField] float Interval = 1f;
    [SerializeField] float MinPitch = 0f;
    [SerializeField] float MaxPitch = 1f;

    private bool m_IntroPlaying = false;
    private bool m_OutroPlaying = false;
    private float m_Timer = 0f;
    private float m_StepSize = 0f;

    void FixedUpdate()
    {
        if (m_IntroPlaying || m_OutroPlaying)
            m_Timer += Time.fixedDeltaTime;

        if (m_IntroPlaying)
        {
            Source.pitch += m_StepSize;
            if (m_Timer >= Interval)
                m_IntroPlaying = false;
        }
        else if (m_OutroPlaying)
        {
            Source.pitch -= m_StepSize;
            if (m_Timer >= Interval)
            {
                m_OutroPlaying = false;
                Source.mute = true;
            }
        }
    }

    public void StartEffect()
    {
        if (Source != null)
        {
            m_Timer = 0f;
            m_StepSize = (MaxPitch - Source.pitch) / ((Interval * 50) + 1);
            Source.mute = false;
            m_IntroPlaying = true;
        }
    }

    public void EndEffect()
    {
        if (Source != null)
        {
            m_Timer = 0f;
            m_StepSize = (Source.pitch - MinPitch) / ((Interval * 50) + 1);
            m_OutroPlaying = true;
        }
    }
}
