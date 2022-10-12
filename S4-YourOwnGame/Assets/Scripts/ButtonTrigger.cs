using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] EffectObjects;
    [SerializeField] bool HasActiveTimer = false;
    [SerializeField] float ActiveTimerSeconds = 0f;

    List<GameObject> collidingObjects = new();

    public bool IsPressed = false;

    public void OnCollisionEnter(Collision collision)
    {
        if (!IsPressed)
        {
            if (collision.gameObject.tag == "Player")
            {
                collidingObjects.Add(collision.gameObject);
                IsPressed = true;
                CancelInvoke(nameof(RevertEffect));
                TriggerEffect();
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (IsPressed)
        {
            if (collision.gameObject.tag == "Player")
            {
                collidingObjects.Remove(collision.gameObject);

                if (collidingObjects.Count == 0)
                {
                    IsPressed = false;
                    if (HasActiveTimer)
                        Invoke(nameof(RevertEffect), ActiveTimerSeconds);
                    else
                        RevertEffect();
                }
            }
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
