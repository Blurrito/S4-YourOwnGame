using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] EffectObjects;
    [SerializeField] bool HasActiveTimer = false;
    [SerializeField] float ActiveTimerSeconds = 0f;

    Timer ActiveTimer;
    bool IsPressed = false;

    public void Start()
    {
        if (HasActiveTimer)
        {
            ActiveTimer = new Timer(ActiveTimerSeconds);
            ActiveTimer.Elapsed += Timer_Elapsed;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (!IsPressed)
            if (collision.gameObject.tag == "Player")
            {
                IsPressed = true;
                if (ActiveTimer != null)
                    ActiveTimer.Stop();
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
                    ActiveTimer.Start();
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
