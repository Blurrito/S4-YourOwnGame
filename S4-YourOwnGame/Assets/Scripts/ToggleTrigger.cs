using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToggleTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] EffectObjects;
    [SerializeField] bool HasObjectTagRestriction = false;
    [SerializeField] string[] TagNames;

    [SerializeField] public GameObject toggleOnState;
    [SerializeField] public GameObject toggleOffState;

    public bool state = false;
    public List<string> Actors = new List<string>();

    private void HandleEnter(GameObject collider)
    {
        if (Actors.Contains(collider.name)) return;

        if (HasObjectTagRestriction && TagNames != null)
        {
            if (TagNames.Contains(collider.tag))
            {
                Actors.Add(collider.name);
                ToggleSwitch();
            }
        }
        else
        {
            Actors.Add(collider.name);
            ToggleSwitch();
        }
    }

    private void HandleExit(GameObject gameObject)
    {
        if (Actors.Contains(gameObject.name))
        {
            Actors.Remove(gameObject.name);
        }
    }

    public void OnCollisionEnter(Collision collision) => HandleEnter(collision.gameObject);
    public void OnTriggerEnter(Collider other) => HandleEnter(other.gameObject);
    public void OnCollisionExit(Collision collision) => HandleExit(collision.gameObject);
    public void OnTriggerExit(Collider other) => HandleExit(other.gameObject);


    private void TriggerEffect()
    {
        if (EffectObjects != null)
        {
            foreach (GameObject EffectObject in EffectObjects)
            {
                TriggerEffect[] Effects = EffectObject.GetComponents<TriggerEffect>();
                foreach (TriggerEffect Effect in Effects)
                {
                    Effect.StartEffect();
                }
            }
        }
    }
    private void RevertEffect()
    {
        if (EffectObjects != null)
        {
            foreach (GameObject EffectObject in EffectObjects)
            {
                TriggerEffect[] Effects = EffectObject.GetComponents<TriggerEffect>();
                foreach (TriggerEffect Effect in Effects)
                {
                    Effect.EndEffect();
                }
            }
        }
    }

    private void ToggleSwitch()
    {
        state = !state;
        toggleOnState.SetActive(state);
        toggleOffState.SetActive(!state);

        if (state)
        {
            TriggerEffect();
        }
        else
        {
            RevertEffect();
        }
    }
}
