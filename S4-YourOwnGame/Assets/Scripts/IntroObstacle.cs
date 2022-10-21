using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroObstacle : MonoBehaviour
{
    private bool ResetTriggered = false;

    private void OnNextObstacleReady()
    {
        Segment Segment = gameObject.GetComponentInParent<Segment>();
        if (Segment != null)
            Segment.OnObstacleSpawnReady();
    }

    private void OnObstaclePass()
    {
        Segment Segment = gameObject.GetComponentInParent<Segment>();
        if (Segment != null)
            Segment.OnObstaclePass();
    }

    private void OnAnimationEnd() => Destroy(gameObject);

    public void OnTriggerEnter(Collider other)
    {
        if (!ResetTriggered && other.gameObject.tag.Equals("Player"))
        {
            ResetTriggered = true;
            Segment Segment = gameObject.GetComponentInParent<Segment>();
            if (Segment != null)
                Segment.OnObstacleFail();
        }
    }
}
