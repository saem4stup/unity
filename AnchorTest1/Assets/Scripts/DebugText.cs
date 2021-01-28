using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textMeshPro = default;
    void Start()
    {
        textMeshPro.text = "test test";
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        textMeshPro.text += logString + "\n";
    }
}
