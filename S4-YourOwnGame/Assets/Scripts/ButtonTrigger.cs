using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] EffectObjects;
    [SerializeField] bool HasActiveTimer = false;
    [SerializeField] float ActiveTimerSeconds = 0f;

    public bool IsPressed = false;

    public void OnCollisionEnter(Collision collision)
    {
        if (!IsPressed)
            if (collision.gameObject.tag == "Player")
            {
                IsPressed = true;
                CancelInvoke(nameof(RevertEffect));
                TriggerEffect();
            }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (IsPressed)
            if (collision.gameObject.tag == "Player")
            {
                IsPressed = false;
                if (HasActiveTimer)
                    Invoke(nameof(RevertEffect), ActiveTimerSeconds);
                else
                    RevertEffect();
            }
    }

    public void TriggerEffect()
    {
        if (EffectObjects != null)
            foreach (GameObject EffectObject in EffectObjects)
            {
                TriggerEffect[] Effects = EffectObject.GetComponents<TriggerEffect>();
                foreach (TriggerEffect Effect in Effects)
                    Effect.StartEffect();
            }
    }

    public void RevertEffect()
    {
        if (EffectObjects != null)
            foreach (GameObject EffectObject in EffectObjects)
            {
                TriggerEffect[] Effects = EffectObject.GetComponents<TriggerEffect>();
                foreach (TriggerEffect Effect in Effects)
                    Effect.EndEffect();
            }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e) => RevertEffect();
}
