using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wip : MonoBehaviour
{
    [SerializeField] int ObstacleCount = 10;
    [SerializeField] GameObject[] Obstacles;

    private int PassedObstacles = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnObstaclePass()
    {

    }

    private void OnObstacleFail()
    {

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
