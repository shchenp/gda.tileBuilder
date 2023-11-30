using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    
    private Camera _camera;
    private MapController _mapController;
    
    private Tile _tile;
    private bool _isTileOnMouse;
    
    private Vector3Int _cellPosition;
    
    private void Awake()
    {
        _camera = Camera.main;
        _mapController = new MapController();
    }

    private void Update()
    {
        if (_tile == null)
        {
            return;
        }

        var mousePosition = Input.mousePosition;
        var ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out var hitInfo))
        {
            PlacingTile(hitInfo, out _cellPosition);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            TrySetTile();
        }
    }
    
    public void StartPlacingTile(GameObject tilePrefab)
    {
        if (_isTileOnMouse)
        {
            Destroy(_tile.gameObject);
        }
        
        var tileObject = Instantiate(tilePrefab);
        _tile = tileObject.GetComponent<Tile>(); 
        _isTileOnMouse = true;
    }

    private void PlacingTile(RaycastHit hitInfo, out Vector3Int cellPosition)
    {
        var worldPosition = hitInfo.point;

        cellPosition = _grid.WorldToCell(worldPosition);
        var cellCenterWorld = _grid.GetCellCenterWorld(cellPosition);
                
        _tile.transform.position = cellCenterWorld;

        var isCellAvailable = _mapController.IsCellAvailable(cellPosition);
        _tile.CheckTileInstallation(isCellAvailable);
    }

    private void TrySetTile()
    {
        if (_mapController.IsCellAvailable(_cellPosition))
        { 
            _mapController.SetCellBusy(_cellPosition);
            
            _tile.SetDefaultColor();
            _tile = null;
            _isTileOnMouse = false;
        }
    }
}