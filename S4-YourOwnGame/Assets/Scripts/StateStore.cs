using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateStore
{
    public ObjectStateStamp stateStamp;

    public AnimatorStore animator;
    public TransformStore transform;
    public ButtonTriggerStore buttonTrigger;

    public StateStore(ObjectStateStamp stateStamp)
    {
        this.stateStamp = stateStamp;
    }
}

public record AnimatorStore 
{
    public List<(string name, bool value)> parameters;
    public int animationName;
    public float timeIndex;
}

public record TransformStore
{
    public Vector3 position;
    public Quaternion rotation;
}

public record ButtonTriggerStore
{
    public bool isPressed;
    public List<string> actors;
}

