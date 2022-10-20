using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wip2 : MonoBehaviour
{
    private bool ResetTriggered = false;

    private void OnAnimationEnd()
    {
        Wip Segment = gameObject.GetComponentInParent<Wip>();
        if (Segment != null)
            Segment.OnObstaclePass();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!ResetTriggered && other.gameObject.tag.Equals("Player"))
        {
            ResetTriggered = true;
            Wip Segment = gameObject.GetComponentInParent<Wip>();
            if (Segment != null)
                Segment.OnObstacleFail();
        }
    }
}
