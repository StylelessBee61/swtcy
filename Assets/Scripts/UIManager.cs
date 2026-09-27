using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIWindow> _uiWindows;

    void Start()
    {
    }

    public void ShowWindow(string windowName)
    {
        foreach (var window in _uiWindows)
        {
            if (window.WindowID == windowName)
            {
                window.Show();
                return;
            }
        }
    }

    public void HideWindow(string windowName)
    {
        foreach (var window in _uiWindows)
        {
            if (window.WindowId == windowName)
            {
                window.Hide();
                return;
            }
        }
    }
}
