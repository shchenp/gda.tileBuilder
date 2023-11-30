using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
    private bool[,] _availableCells = new bool[10, 10];

    public MapController()
    {
        for (var i = 0; i < _availableCells.GetLength(0); i++)
        {
            for (var j = 0; j < _availableCells.GetLength(1); j++)
            {
                _availableCells[i, j] = true;
            }
        }
    }

    public bool IsCellAvailable(Vector3Int cell)
    {
        return IsTileOnMap(cell) && _availableCells[cell.x, cell.z];
    }

    public void SetCellBusy(Vector3Int cell)
    {
        _availableCells[cell.x, cell.z] = false;
    }

    private bool IsTileOnMap(Vector3Int cell)
    {
        if (cell.x < 0 || cell.z < 0 || cell.x >= _availableCells.GetLength(0) || cell.z >= _availableCells.GetLength(1))
        {
            return false;
        }

        return true;
    }
}
