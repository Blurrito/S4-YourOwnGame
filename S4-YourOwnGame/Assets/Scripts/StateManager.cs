using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    private void Start()
    {
        if (instance != null)
        {
            Debug.LogError("Scene can not contain multiple StateManagers");
        }

        instance = this;
    }

    public void SaveAllStates(ObjectStateStamp stateStamp)
    {
        BroadcastMessage("SaveState", stateStamp, SendMessageOptions.DontRequireReceiver);
    }

    public void LoadAllStates(ObjectStateStamp stateStamp)
    {
        BroadcastMessage("LoadState", stateStamp, SendMessageOptions.DontRequireReceiver);
    }
}

public enum ObjectStateStamp
{
    checkpoint,
    recording
}
