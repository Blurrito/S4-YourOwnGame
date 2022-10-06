using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] EffectObjects;
    [SerializeField] bool HasActiveTimer = false;
    [SerializeField] float ActiveTimerSeconds = 0f;

    bool IsPressed = false;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
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
        Debug.Log(collision.gameObject.tag);
        if (IsPressed)
            if (collision.gameObject.tag =="Player")
            {
                IsPressed = false;
                if (HasActiveTimer)
                    Invoke(nameof(RevertEffect), ActiveTimerSeconds);
                else
                    RevertEffect();
            }
    }

    private void TriggerEffect()
    {
        if (EffectObjects != null)
            foreach (GameObject EffectObject in EffectObjects)
            {
                TriggerEffect[] Effects = EffectObject.GetComponents<TriggerEffect>();
                foreach (TriggerEffect Effect in Effects)
                    Effect.StartEffect();
            }
    }

    private void RevertEffect()
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
