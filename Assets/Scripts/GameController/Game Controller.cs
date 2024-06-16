using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Location> _locations;
    public Location CurrentLocation;

    public BackgroundController BackgroundController;
    public EnemiesController EnemiesController;

    private List<int> _bannedIndexes;
    private int _locationIndex;

    private bool _locationLoading;

    private void Awake()
    {
        LoadNextLocation();
    }

    private void Update()
    {
        if(EnemiesController.EnemyIsOver() && !_locationLoading)
        {
            _locationLoading = true;
            LoadNextLocation();
        }
    }

    private void LoadNextLocation()
    {
        if(CurrentLocation != null)
        {
            ClearLocation();
        }

        do
        {
            _locationIndex = Random.Range(0, _locations.Count);
        } while (!CheckLocationIndex(_locationIndex));

        CurrentLocation = _locations[_locationIndex];
        CreateLocation();
    }

    public void CreateLocation()
    {
        BackgroundController.BackgroundPattern = CurrentLocation.ReturnBackgroundPattern();
        BackgroundController.CreateLayers();

        EnemiesController.EnemiesPattern = CurrentLocation.ReturnEnemiesPattern();
        EnemiesController.SpawnEnemy();

        _locationLoading = false;
    }

    public void ClearLocation()
    {
        BackgroundController.ClearLayers();
    }

    private bool CheckLocationIndex(int index)
    {
        if (_bannedIndexes.Count == _locations.Count)
        {
            _bannedIndexes.Clear();
            return true;
        }

        for (int i = 0; i < _bannedIndexes.Count; i++)
        {
            if(index == _bannedIndexes[i])
            {
                return false;
            }
        }

        _bannedIndexes.Add(index);
        return true;
    }

}
