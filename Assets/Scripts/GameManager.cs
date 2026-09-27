using System;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager UIManager;
    public string uiToShow = "settingsUI";

    [Button]
    private void Test()
    {
        UIManager.ShowWindow(uiToShow);
    }
}

