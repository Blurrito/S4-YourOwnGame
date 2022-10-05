using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    private void Start()
    {
        instance = this;
    }

    public void SaveAllStates(ObjectStateStamp stateStamp)
    {
        BroadcastMessage("SaveState", stateStamp);
    }

    public void LoadAllStates(ObjectStateStamp stateStamp)
    {
        BroadcastMessage("LoadState", stateStamp);
    }
}

public enum ObjectStateStamp
{
    checkpoint,
    recording
}
