using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateSaver : MonoBehaviour
{
    [SerializeField] List<Component> componentsToSave;
    StateStore stateStore = new();

    //called on recording creator start
    [ContextMenu("Save state")]
    public void SaveState()
    {
        foreach (Component item in componentsToSave)
        {
            SaveComponent(item);
        }
    }

    //called on cancel/retry recording
    [ContextMenu("Load state")]
    public void LoadState()
    {
        foreach (Component item in componentsToSave)
        {
            LoadComponent(item);
        }
    }

    private void SaveComponent(Component component)
    {
        Type t = component.GetType();

        if (t == typeof(Animator))
        {
            Animator x = (Animator)component;

            List<(string name, bool value)> parameters = new();
            foreach (AnimatorControllerParameter param in x.parameters)
            {
                parameters.Add((param.name, x.GetBool(param.name)));
            };

            stateStore.animator = new AnimatorStore()
            {
                animationName = x.GetCurrentAnimatorClipInfo(0)[0].clip.name,
                parameters = parameters,
                timeIndex = x.GetCurrentAnimatorStateInfo(0).normalizedTime
            };
        }
        else if (t == typeof(Transform))
        {
            Transform x = (Transform)component;

            stateStore.transform = new TransformStore()
            {
                position = x.position,
                rotation = x.rotation
            };
        }
        else
        {
            Debug.LogError($"Saving state of component '{component.GetType().Name}' not implemented yet!");
        }
    }

    private void LoadComponent(Component component)
    {
        Type t = component.GetType();

        if (t == typeof(Animator))
        {
            Animator a = (Animator)component;
            
            foreach (var param in stateStore.animator.parameters)
            {
                a.SetBool(param.name, param.value);
            }
            a.Play(stateStore.animator.animationName, 0, stateStore.animator.timeIndex);
        }
        else if (t == typeof(Transform))
        {
            Transform x = (Transform)component;

            x.SetPositionAndRotation(stateStore.transform.position, stateStore.transform.rotation);
        }
        else
        {
            Debug.LogError($"Loading state of component '{component.GetType().Name}' not implemented yet!");
        }
    }
}
