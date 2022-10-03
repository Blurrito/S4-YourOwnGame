using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTransformEffect : TriggerEffect
{
    [SerializeField] Vector3 Position;
    [SerializeField] Quaternion Rotation;
    [SerializeField] Vector3 Scale;

    Vector3 BasePosition;
    Quaternion BaseRotation;
    Vector3 BaseScale;

    void Start()
    {
        BasePosition = transform.position;
        BaseRotation = transform.rotation;
        BaseScale = transform.localScale;
    }

    public override void StartEffect()
    {
        transform.position = Position;
        transform.rotation = Rotation;
        transform.localScale = Scale;
    }

    public override void EndEffect()
    {
        transform.position = BasePosition;
        transform.rotation = BaseRotation;
        transform.localScale = BaseScale;
    }
}
