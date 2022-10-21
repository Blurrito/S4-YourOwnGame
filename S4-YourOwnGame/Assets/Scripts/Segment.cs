using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] int ObstacleCount = 10;
    [SerializeField] GameObject[] Obstacles;
    [SerializeField] float OnSegmentStartInterval = 1f;
    [SerializeField] float OnSegmentSuccessInterval = 1f;
    [SerializeField] float OnSegmentFailInterval = 1f;

    [SerializeField] TriggerEffect[] OnSegmentStartEffects;
    [SerializeField] TriggerEffect[] OnSegmentSuccessEffects;
    [SerializeField] TriggerEffect[] OnSegmentFailEffects;

    private GameObject[] ObstacleOrder;
    private int SpawnedObstacles = 0;
    private int PassedObstacles = 0;
    private bool SegmentEnded = false;

    void Start()
    {
        RandomizeArray(Obstacles, out ObstacleOrder, ObstacleCount);
    }

    public void OnSegmentStart()
    {
        SegmentEnded = false;
        TriggerEffects(OnSegmentStartEffects, false);
        Invoke(nameof(InstantiateObstacle), OnSegmentStartInterval);
    }

    public void OnObstacleSpawnReady()
    {
        if (++SpawnedObstacles < ObstacleCount && !SegmentEnded)
            InstantiateObstacle();
    }

    public void OnObstaclePass()
    {
        if (++PassedObstacles == ObstacleCount && !SegmentEnded)
            OnSegmentEnd();
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
            TriggerEffects(OnSegmentSuccessEffects, false);
            Invoke(nameof(AdvanceToNextSegment), OnSegmentSuccessInterval);
        }
    }

    private void ResetSegment()
    {
        SegmentEnded = true;
        RandomizeArray(Obstacles, out ObstacleOrder, ObstacleCount);
        TriggerEffects(OnSegmentFailEffects, false);
        Invoke(nameof(OnSegmentStart), OnSegmentFailInterval);
    }

    private void InstantiateObstacle() => Instantiate(ObstacleOrder[SpawnedObstacles], gameObject.transform, false);

    private void TriggerEffects(TriggerEffect[] Effects, bool Revert)
    {
        if (Effects != null)
            foreach (TriggerEffect Effect in Effects)
                if (Revert)
                    Effect.EndEffect();
                else
                    Effect.StartEffect();
    }
    
    private void AdvanceToNextSegment()
    {
        SegmentManager Manager = gameObject.GetComponentInParent<SegmentManager>();
        if (Manager != null)
            Manager.OnSegmentPass();
    }

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
