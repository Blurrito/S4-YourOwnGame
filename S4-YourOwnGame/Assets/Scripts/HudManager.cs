using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] GameObject recordTimeBackground;
    [SerializeField] TextMeshProUGUI recordTimeText;

    private void Start()
    {
        recordTimeBackground.SetActive(false);
    }

    public void StartTimer(int seconds)
    {
        recordTimeText.text = $"{seconds}";
    }
}
