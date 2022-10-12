using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRecord
{
    public Vector3 position;
    public Quaternion rotation;
    public List<AnimationRecord> animationRecords = new List<AnimationRecord>();
    public bool IsGrounded;

    public TransformRecord(Transform t, bool isGrounded)
    {
        position = t.position;
        rotation = t.rotation;
        IsGrounded = isGrounded;
    }
}

public class AnimationRecord
{
    public int name;
    public bool state;
    public float numState = -1;
    public bool isBool;

    public AnimationRecord(int _name, bool _state)
    {
        name = _name;
        state = _state;
        isBool = true;
    }

    public AnimationRecord(int _name, float _state)
    {
        name = _name;
        numState = _state;
        isBool = false;
    }
}
