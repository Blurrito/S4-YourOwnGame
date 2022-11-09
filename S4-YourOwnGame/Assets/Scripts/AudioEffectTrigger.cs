using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectTrigger : MonoBehaviour
{
    [SerializeField] AudioEffect[] Effects;

    public void StartAudioEffects()
    {
        foreach (AudioEffect Effect in Effects)
            Effect.StartEffect();
    }

    public void EndAudioEffects()
    {
        foreach (AudioEffect Effect in Effects)
            Effect.EndEffect();
    }
}
