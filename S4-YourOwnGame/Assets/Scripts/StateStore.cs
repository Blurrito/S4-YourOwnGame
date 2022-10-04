using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateStore
{
    public AnimatorStore animator;
    public TransformStore transform;
}

public record AnimatorStore 
{
    public List<(string name, bool value)> parameters;
    public string animationName;
    public float timeIndex;
}

public record TransformStore
{
    public Vector3 position;
    public Quaternion rotation;
}

