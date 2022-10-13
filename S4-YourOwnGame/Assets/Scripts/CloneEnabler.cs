using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneEnabler : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            CloneManager Manager = other.gameObject.GetComponent<CloneManager>();
            if (Manager != null)
                Manager.canUseClones = true;
        }
        Destroy(gameObject);
    }
}
