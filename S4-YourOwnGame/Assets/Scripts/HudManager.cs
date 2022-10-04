using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;

    [SerializeField] GameObject recordTimeBackground;
    [SerializeField] TextMeshProUGUI recordTimeText;

    private void Start()
    {
        instance = this;
        recordTimeBackground.SetActive(false);
    }

    public void SetTimer(int seconds)
    {
        recordTimeText.text = $"{seconds}";
    }

    public void ActivateTimer(bool toggle)
    {
        recordTimeBackground.SetActive(toggle);
    }
}
