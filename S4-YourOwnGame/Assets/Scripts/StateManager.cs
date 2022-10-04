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

    [ContextMenu("Save all states")]
    public void SaveAllStates()
    {
        BroadcastMessage("SaveState");
    }

    [ContextMenu("Load all states")]
    public void LoadAllStates()
    {
        BroadcastMessage("LoadState");
    }
}
