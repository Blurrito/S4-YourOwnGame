using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wip3 : MonoBehaviour
{
    [SerializeField] float Interval = 1f;
    [SerializeField] Wip[] Segments;

    private bool SegmentStarted = false;
    private int CurrentSegment = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        CurrentSegment++;
        Invoke(nameof(StartNextSegment), Interval);
    }

    private void StartNextSegment() => Segments[CurrentSegment].OnSegmentStart();
}
