using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    [SerializeField] float Interval = 1f;
    [SerializeField] Segment[] Segments;

    private bool SegmentStarted = false;
    private int CurrentSegment = 0;

    public void OnTriggerEnter(Collider other)
    {
        if (!SegmentStarted)
        {
            SegmentStarted = true;
            StartNextSegment();
        }
    }

    public void OnSegmentPass()
    {
        if (++CurrentSegment < Segments.Length)
            Invoke(nameof(StartNextSegment), Interval);
    }

    private void StartNextSegment() => Segments[CurrentSegment].OnSegmentStart();
}
