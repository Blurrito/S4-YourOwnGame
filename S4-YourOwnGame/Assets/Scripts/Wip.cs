using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wip : MonoBehaviour
{
    [SerializeField] int ObstacleCount = 10;
    [SerializeField] GameObject[] Obstacles;
    [SerializeField] float OnSegmentStartInterval = 1f;

    private GameObject[] ObstacleOrder;
    private int PassedObstacles = 0;
    private bool SegmentEnded = false;

    void Start()
    {
        RandomizeArray(Obstacles, out ObstacleOrder, ObstacleCount);
    }

    public void OnSegmentStart()
    {
        SegmentEnded = false;
    }

    public void OnObstaclePass()
    {
        if (++PassedObstacles == ObstacleCount)
            OnSegmentEnd();
        else
            InstantiateObstacle();
    }
    
    public void OnObstacleFail()
    {
        if (!SegmentEnded)
            ResetSegment();
    }

    private void OnSegmentEnd()
    {
        if (!SegmentEnded)
        {
            SegmentEnded = true;
            Wip3 Manager = gameObject.GetComponentInParent<Wip3>();
            if (Manager != null)
                Manager.OnSegmentPass();
        }
    }

    private void ResetSegment()
    {
        SegmentEnded = true;
        RandomizeArray(Obstacles, out ObstacleOrder, ObstacleCount);
        Invoke(nameof(OnSegmentStart), OnSegmentStartInterval);
    }

    private void InstantiateObstacle() => Instantiate(ObstacleOrder[PassedObstacles], gameObject.transform, false);

    private void RandomizeArray<T>(T[] InputArray, out T[] OutputArray, int OutputArraySize) where T : class
    {
        T[] Out = new T[OutputArraySize];
        T[] Randomized = new T[InputArray.Length];
        Array.Copy(InputArray, Randomized, InputArray.Length);

        int Index = 0;
        while (Index < OutputArraySize)
        {
            System.Random Random = new System.Random();
            Randomized = Randomized.OrderBy(x => Random.Next()).ToArray();
            Array.Copy(Randomized, 0, Out, Index, Math.Min(Randomized.Length, Out.Length - Index));
            Index += Math.Min(Randomized.Length, Out.Length - Index);
        }
        OutputArray = Out;
    }
}
