using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] 
    private Renderer[] _renderers;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
    }

    private void SetColor(Color color)
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.color = color;
        }
    }

    public void SetDefaultColor()
    {
        SetColor(Color.white);
    }

    public void CheckTileInstallation(bool isAvailable)
    {
        SetColor(isAvailable ? Color.green : Color.red);
    }
}
