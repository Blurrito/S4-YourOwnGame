using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSetter : MonoBehaviour
{
    [SerializeField] string Target;
    [SerializeField] GameObject PlayerObject;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(Target))
            PlayerObject.transform.parent = transform.parent.transform;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals(Target))
            PlayerObject.transform.parent = null;
    }
}
