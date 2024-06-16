using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Create New Location")]
public class Location : ScriptableObject
{
    [SerializeField] private BackgroundPattern _backgroundPattern;
    [SerializeField] private GameObject[] _enemiesPattern;
    [SerializeField] private AudioClip[] _locationTracks;

    public BackgroundPattern ReturnBackgroundPattern()
    {
        return _backgroundPattern;
    }
    public GameObject[] ReturnEnemiesPattern()
    {
        return _enemiesPattern;
    }
    public AudioClip[] ReturnLocationTracks()
    {
        return _locationTracks;
    }
}
