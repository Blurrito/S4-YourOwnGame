using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class IntroObstacleSpawner : MonoBehaviour
{
    [SerializeField] float ObstacleSpawnInterval = 60f;
    [SerializeField] int WalkObstacleSpawnCount = 10;
    [SerializeField] int JumpObstacleSpawnCount = 10;

    [SerializeField] GameObject ObstacleStartLift;
    [SerializeField] GameObject ObstacleEndLift;
    [SerializeField] GameObject Stairs;
    [SerializeField] GameObject[] WalkObstacles;
    [SerializeField] GameObject[] JumpObstacles;

    private GameObject[] RandomizedWalkObstacles;
    private GameObject[] RandomizedJumpObstacles;

    private bool WalkPhaseEnded = false;
    private bool JumpPhaseStarted = false;
    private bool IsFinished = false;
    private int CurrentWalkObstacle = 0;
    private int CurrentJumpObstacle = 0;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeJumpObstacleList();
        RandomizeWalkObstacleList();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!IsFinished)
            if (other.gameObject.tag.Equals("Player"))
            {
                InvokeRepeating(nameof(SpawnNextWalkObstacle), 0, ObstacleSpawnInterval);
                SetLiftAnimationState(true);
            }
        SetColliderEnabledStatus(false);
    }

    public void OnPlayerDeath() => ResetSpawner();

    public void ResetSpawner()
    {
        Debug.Log("Reset");
        if (!IsFinished)
        {
            SetLiftAnimationState(false);
            JumpPhaseStarted = false;
            WalkPhaseEnded = false;
            CurrentWalkObstacle = 0;
            CurrentJumpObstacle = 0;
            SetColliderEnabledStatus(true);
        }
    }

    private void ObstacleTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (!JumpPhaseStarted && !WalkPhaseEnded)
            SpawnNextWalkObstacle();
        else if (!JumpPhaseStarted)
            JumpPhaseStarted = true;
        else if (!IsFinished)
            SpawnNextJumpObstacle();

    }

    private void SpawnNextWalkObstacle()
    {
        Instantiate(RandomizedWalkObstacles[CurrentWalkObstacle++], new Vector3(0, -2, -18), Quaternion.identity);
        if (CurrentWalkObstacle >= RandomizedWalkObstacles.Length)
        {
            CancelInvoke(nameof(SpawnNextWalkObstacle));
            WalkPhaseEnded = true;
            InvokeRepeating(nameof(SpawnNextJumpObstacle), 5, ObstacleSpawnInterval);
        }
    }

    private void SpawnNextJumpObstacle()
    {
        Instantiate(RandomizedJumpObstacles[CurrentJumpObstacle++], new Vector3(0, -2, -18), Quaternion.identity);
        if (CurrentJumpObstacle >= RandomizedJumpObstacles.Length)
        {
            CancelInvoke(nameof(SpawnNextJumpObstacle));
            SetLiftAnimationState(false);
            Invoke(nameof(TriggerStairsAnimation), 3);
            IsFinished = true;
        }
    }

    private void SetColliderEnabledStatus(bool Status)
    {
        BoxCollider Collider = gameObject.GetComponent<BoxCollider>();
        if (Collider != null)
            Collider.enabled = Status;
    }

    private void SetLiftAnimationState(bool State)
    {
        Animator StartLiftAnimator = ObstacleStartLift.GetComponent<Animator>();
        Animator EndLiftAnimator = ObstacleEndLift.GetComponent<Animator>();
        if (StartLiftAnimator != null)
            StartLiftAnimator.SetBool("IsActive", State);
        if (EndLiftAnimator != null)
            EndLiftAnimator.SetBool("IsActive", State);
    }

    private void TriggerStairsAnimation()
    {
        Animator StairsAnimator = Stairs.GetComponent<Animator>();
        if (StairsAnimator != null)
            StairsAnimator.SetBool("IsFinished", true);
    }

    private void RandomizeWalkObstacleList()
    {
        GameObject[] Temp = new GameObject[WalkObstacles.Length];
        Array.Copy(WalkObstacles, Temp, Temp.Length);
        RandomizedWalkObstacles = new GameObject[WalkObstacleSpawnCount];

        int Index = 0;
        while (Index < RandomizedWalkObstacles.Length)
        {
            System.Random Random = new System.Random();
            Temp = Temp.OrderBy(x => Random.Next()).ToArray();
            Array.Copy(Temp, 0, RandomizedWalkObstacles, Index, Math.Min(Temp.Length, RandomizedWalkObstacles.Length - Index));
            Index += Math.Min(Temp.Length, RandomizedWalkObstacles.Length - Index);
        }
    }

    private void RandomizeJumpObstacleList()
    {
        GameObject[] Temp = new GameObject[JumpObstacles.Length];
        Array.Copy(JumpObstacles, Temp, Temp.Length);
        RandomizedJumpObstacles = new GameObject[JumpObstacleSpawnCount];

        int Index = 0;
        while (Index < RandomizedJumpObstacles.Length)
        {
            System.Random Random = new System.Random();
            Temp = Temp.OrderBy(x => Random.Next()).ToArray();
            Array.Copy(Temp, 0, RandomizedJumpObstacles, Index, Math.Min(Temp.Length, RandomizedJumpObstacles.Length - Index));
            Index += Math.Min(Temp.Length, RandomizedJumpObstacles.Length - Index);
        }
    }
}
